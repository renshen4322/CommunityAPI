using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace Community.OAuth.Attribute
{
    // Summary:
    //     An attribute that specifies that an action parameter comes from the URI of
    //     the incoming System.Net.Http.HttpRequestMessage.
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
    public sealed class FromOauthAttribute : ModelBinderAttribute
    {
        // Summary:
        //     Initializes a new instance of the System.Web.Http.FromUriAttribute class.
        public FromOauthAttribute(){
        
        }

        // Summary:
        //     Gets the value provider factories for the model binder.
        //
        // Parameters:
        //   configuration:
        //     The configuration.
        //
        // Returns:
        //     A collection of System.Web.Http.ValueProviders.ValueProviderFactory objects.
        public override IEnumerable<System.Web.Http.ValueProviders.ValueProviderFactory> GetValueProviderFactories(HttpConfiguration configuration)
        {
            return base.GetValueProviderFactories(configuration);
        }
    }
}
