using Community.OAuth.ApiValueProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.ValueProviders;

namespace Community.OAuth.ApiValueProviderFactory
{
    public class OauthProviderFactory : ValueProviderFactory
    {
        public override IValueProvider GetValueProvider(HttpActionContext actionContext)
        {
            return new OauthValueProvider(actionContext);
        }
    }
}
