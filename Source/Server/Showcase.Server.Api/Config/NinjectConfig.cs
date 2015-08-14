namespace Showcase.Server.Api.Config
{
    using System;
    using System.Data.Entity;
    using System.Web;

    using Ninject;
    using Ninject.Extensions.Conventions;
    using Ninject.Web.Common;

    using Showcase.Data;
    using Showcase.Data.Common.Repositories;

    using Showcase.Services.Common;
    using Showcase.Services.Logic;

    using ServerConstants = Showcase.Server.Common.Constants;

    public static class NinjectConfig 
    {
        public static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                ObjectFactory.Initialize(kernel);
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind(typeof(IRepository<>)).To(typeof(EfGenericRepository<>));
            kernel.Bind<DbContext>().To<ShowcaseDbContext>().InRequestScope();

            kernel.Bind(k => k
                .From(
                    ServerConstants.InfrastructureAssembly,
                    ServerConstants.DataServicesAssembly,
                    ServerConstants.LogicServicesAssembly)
                .SelectAllClasses()
                .InheritedFrom<IService>()
                .BindDefaultInterface());
        }        
    }
}
