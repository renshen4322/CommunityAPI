using Community.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.ValueProviders;
using Community.Utils.Common;

namespace Community.OAuth.ApiValueProvider
{
    public class OauthValueProvider : IValueProvider
    {
        private Dictionary<string, string> _values;
        private string _userId;
        private string _token;
       
        public OauthValueProvider(HttpActionContext actionContext)
        {
            
            if (actionContext == null)
            {
                throw new ArgumentNullException("actionContext");
            }
            _token = actionContext.Request.Headers.Authorization.Parameter;
            
        }
        public bool ContainsPrefix(string prefix)
        {

            return false;
        }

        public ValueProviderResult GetValue(string key)
        {
            string[] keys = key.Split('.');           
            
            if(keys.Contains("UserId"))
            {
                OAuthToken token = CacheHelper.Get<OAuthToken>(_token);
                return new ValueProviderResult(12, "12", CultureInfo.InvariantCulture);
            }
            return null;
        }


    }
}
