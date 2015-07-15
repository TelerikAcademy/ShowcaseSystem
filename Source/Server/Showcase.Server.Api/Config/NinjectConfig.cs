namespace Showcase.Server.Api
{
    using System;
    using System.Data.Entity;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Extensions.Conventions;
    using Ninject.Web.Common;

    using Showcase.Data;
    using Showcase.Data.Common.Repositories;

    using Showcase.Server.Common;
    using Showcase.Services.Common;
    using Showcase.Services.Data;
    using Showcase.Services.Data.Contracts;

    using ServerConstants = Showcase.Server.Common.Constants;

    public static class NinjectConfig 
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        public static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind(typeof(IRepository<>)).To(typeof(EfGenericRepository<>));
            kernel.Bind<DbContext>().To<ShowcaseDbContext>().InRequestScope();

            kernel.Bind(k => k
                .From(ServerConstants.DataServicesAssembly, ServerConstants.LogicServicesAssembly)
                .SelectAllClasses()
                .InheritedFrom<IService>()
                .BindDefaultInterface());
        }        
    }
}
