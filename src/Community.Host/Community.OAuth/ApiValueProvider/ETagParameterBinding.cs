using Community.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;

namespace Community.OAuth.ApiValueProvider
{
    public class ETagParameterBinding : HttpParameterBinding
    {
        public ETagParameterBinding(HttpParameterDescriptor parameter)
            : base(parameter)
        {

        }
        public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider,
            HttpActionContext actionContext, CancellationToken cancellationToken)
        {

            var oauth = new CommandRequestDto { };
          var qq=   actionContext.ActionArguments["dto"] as CommandRequestDto;       
            var tsc = new TaskCompletionSource<object>();
            tsc.SetResult(null);
            return tsc.Task;
        }
    }
    public class OAutht
    {
        public string qq { get; set; }
    }
}
