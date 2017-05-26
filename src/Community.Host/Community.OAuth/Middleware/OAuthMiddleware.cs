
using Microsoft.Owin;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Community.OAuth.Middleware
{
    public class OAuthMiddleware : OwinMiddleware
    {
        const string BEARER = "Bearer";
        readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public OAuthMiddleware(OwinMiddleware next)
            : base(next)
        {

        }
        public override Task Invoke(IOwinContext context)
        {
            try
            {
                string token = string.Empty;
                token = context.Request.Headers["Authorization"] != null ? context.Request.Headers["Authorization"] : null;
                if (!string.IsNullOrEmpty(token))
                {
                    IOAuth oautn = new OAuth();
                    OAuthToken oauthToken = oautn.AcquireToken(token);
                    if (oauthToken.Error == null)
                    {
                        oauthToken.IsAuthenticated = true;
                        SetPrincipal(context, new UserPrincipal(oauthToken));
                    }
                    else
                    {

                        context.Response.Headers.Append("WWW-Authenticate", oauthToken.Error.Msg);
                        context.Response.StatusCode = 401;
                        return Task.FromResult(0);
                    }
                }

                return Next.Invoke(context);
            }
            catch (System.Exception ex)
            {
                // _logger.Debug("Ending request with code {0}, message {1}...", 401, MISSING_AUTH);
                _logger.Error("OwinMiddleware exception because:{0},{1}",ex.Message , ex.StackTrace);
                context.Response.StatusCode = 401;
                return Task.FromResult(0);
            }

        }
        private void SetPrincipal(IOwinContext context, IPrincipal principal)
        {

            Thread.CurrentPrincipal = principal;
            if (context.Request != null)
            {
                context.Request.User = principal;
            }
        }


    }
}
