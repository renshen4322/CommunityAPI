using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http;
using System.Net.Http;

namespace Community.OAuth.Attribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true)]
    public class OAuthRequestAttribute : ActionFilterAttribute
    {
       
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
           
            string token = string.Empty;
token=actionContext.Request.Headers.Authorization!=null?actionContext.Request.Headers.Authorization.Scheme+" "+actionContext.Request.Headers.Authorization.Parameter:null;
        
         IOAuth oautn = new OAuth();
         OAuthToken oauthToken= oautn.AcquireToken(token);
            
         if (oauthToken.Error!=null)
         {
             HandleUnauthorizedRequest(oauthToken.Error.Msg);
         }
        }

        protected void HandleUnauthorizedRequest(string errMsg)
        {
         
            var challengeMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            challengeMessage.Headers.Add("www-Authenticate", errMsg);
            throw new HttpResponseException(challengeMessage);
        }
    }
}
