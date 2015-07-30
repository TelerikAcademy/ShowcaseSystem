namespace Showcase.Services.Common.Bindings
{
    using System;

    using Ninject;

    public class ObjectFactory : IObjectFactory
    {
        private static IKernel staticKernel;
        private IKernel kernel;

        public static void Initialize(IKernel staticKernelInstance)
        {
            staticKernel = staticKernelInstance;
        }

        public ObjectFactory(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public T GetInstance<T>()
        {
            return this.kernel.Get<T>();
        }

        public object GetInstance(Type type)
        {
            return this.kernel.Get(type);
        }

        public T TryGetInstance<T>()
        {
            return this.kernel.TryGet<T>();
        }

        public object TryGetInstance(Type type)
        {
            return this.kernel.TryGet(type);
        }

        public static T Get<T>()
        {
            return staticKernel.Get<T>();
        }

        public static object Get(Type type)
        {
            return staticKernel.Get(type);
        }

        public static T TryGet<T>()
        {
            return staticKernel.TryGet<T>();
        }

        public static object TryGet(Type type)
        {
            return staticKernel.TryGet(type);
        }
    }
}