using Community.Host.App_Start;
using Community.Host.Filter;
using Community.OAuth.Middleware;
using Microsoft.Owin.Cors;
using Owin;
using System.Threading.Tasks;
using System.Web.Cors;
using System.Web.Http;

namespace Community.Host
{
    public class Startup
    {


        public void Configuration(IAppBuilder appBuilder)
        {

     
                HttpConfiguration httpConfiguration = new HttpConfiguration();
               
                //oauth verify
                appBuilder.Use(typeof(OAuthMiddleware));

                #region Cors
                var corsPolicy = new CorsPolicy
                {
                    AllowAnyMethod = true,
                    AllowAnyHeader = true,
                };
                corsPolicy.AllowAnyOrigin = true;              
                var corsOptions = new CorsOptions
                {
                    PolicyProvider = new CorsPolicyProvider
                    {
                        PolicyResolver = context => Task.FromResult(corsPolicy)
                    }
                };
                appBuilder.UseCors(corsOptions);
                #endregion
                //AutoMapper
                AutoMapperConfig.Configure();
                //route
                WebApiConfig.Register(httpConfiguration);

                //customer filter
                httpConfiguration.Filters.Add(new ModelValidationAttribute());
                httpConfiguration.Filters.Add(new LogExceptionAttribute());
                httpConfiguration.Filters.Add(new UserValidationAttribute());


              //  httpConfiguration.Services.Add(typeof(IExceptionLogger), new ErroLogger());
                //swagger
                SwaggerConfig.Register(httpConfiguration);
                //autofac
                AutoFacConfig.Register(appBuilder, httpConfiguration);
                appBuilder.UseWebApi(httpConfiguration);
           




            //------------------------------

        }
    }
}