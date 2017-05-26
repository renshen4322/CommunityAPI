using Community.Common;
using Community.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Community.Host.Extentions
{
    public static class ApiControllerExtentions
    {
        public static long? GetUserId(this ApiController controller)
        {
           var token= controller.Request.Headers.Authorization.Parameter;
          return  CacheHelper.Get<OAuthToken>(token).User;
        }
    }
}