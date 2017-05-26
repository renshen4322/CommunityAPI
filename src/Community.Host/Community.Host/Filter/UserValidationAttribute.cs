using Community.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;


namespace Community.Host.Filter
{
     [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true)]
    public class UserValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (HttpContext.Current.User!=null&&
                HttpContext.Current.User.Identity!=null&&
                HttpContext.Current.User.Identity.IsAuthenticated &&
                (actionContext.Request.RequestUri.AbsolutePath != "/v1/user"&&
                !actionContext.Request.RequestUri.AbsolutePath.Contains("/user/share")
                )&&
                actionContext.Request.Method==HttpMethod.Get)
            {
                var oauthUser = HttpContext.Current.User.Identity as OAuthToken;
                if (oauthUser.UserId == null) {
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "");                   
                }
            }
          

        }
    }
}