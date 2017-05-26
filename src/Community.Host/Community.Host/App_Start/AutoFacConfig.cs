using Autofac;
using Autofac.Integration.WebApi;
using Community.Common;
using Community.Core.Data;
using Community.EntityFramework;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace Community.Host.App_Start
{
    public class AutoFacConfig
    {
        public static void Register(IAppBuilder app,HttpConfiguration config)
        {
            ContainerBuilder builder = new ContainerBuilder();
        
            //data layer
            var dataSettingsManager = new DataSettingsManager();
            var dataProviderSettings = dataSettingsManager.LoadSettings(GlobalAppSettings.DataProvider, GlobalAppSettings.MysqlConnection);
            builder.Register(c => dataProviderSettings).As<DataSettings>();
            builder.Register(x => new EfDataProviderManager(x.Resolve<DataSettings>())).As<BaseDataProviderManager>().InstancePerDependency();


            builder.Register(x => x.Resolve<BaseDataProviderManager>().LoadDataProvider()).As<IDataProvider>().InstancePerDependency();

            if (dataProviderSettings != null && dataProviderSettings.IsValid())
            {
                var efDataProviderManager = new EfDataProviderManager(dataProviderSettings);
                var dataProvider = efDataProviderManager.LoadDataProvider();
                dataProvider.InitConnectionFactory();

                builder.Register<IDbContext>(c => new NopObjectContext(dataProviderSettings.DataConnectionString)).InstancePerLifetimeScope();
            }
            else
            {
                builder.Register<IDbContext>(c => new NopObjectContext(dataProviderSettings.DataConnectionString)).InstancePerLifetimeScope();
            }
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.Register<IDapperRepository>(c => new MysqlDapperRepository(dataProviderSettings.DataConnectionString)).InstancePerLifetimeScope();
            
            //services
            var Services = Assembly.Load("Community.Service");
             var IServices = Assembly.Load("Community.IService");
           
            builder.RegisterAssemblyTypes(IServices,Services)
                    .Where(t => t.Name.EndsWith("Service"))
                    .AsImplementedInterfaces();
            


            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();
    



            var container = builder.Build();       
        
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            // Register the Autofac middleware FIRST, then the Autofac Web API middleware,
            // and finally the standard Web API middleware.
            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);


        }
    }
}