//using Castle.Core;
//using Castle.MicroKernel.Registration;
//using Castle.Windsor;
//using Castle.Windsor.Installer;
//using Framework.Attribute;

//namespace Ybm.Infrastructure.Core
//{
//    public static class ContainerManager
//    {
//        private static object locjObject = new object();

//        private static IWindsorContainer container = null;
//        public static IWindsorContainer Container
//        {
//            get
//            {
//                if (container == null)
//                {
//                    lock (locjObject)
//                    {
//                        // run installers, set _container = new container
//                        BootstrapContainer();
//                    }
//                }
//                return container;
//            }
//        }

//        public static void BootstrapContainer()
//        {

//            if (container != null)
//                return;

//            container = new WindsorContainer();//.Install(FromAssembly.InThisApplication());

//        //    container.Install(assemblyNames.Select(
//        //x => (IWindsorInstaller)new AssemblyInstaller(Assembly.Load(x),
//        //                                              new InstallerFactory()))
//        //                                  .ToArray());

//            container.Kernel.ComponentRegistered += Kernel_ComponentRegistered;

//            var appDomain = System.AppDomain.CurrentDomain;
//            var basePath = appDomain.RelativeSearchPath ?? appDomain.BaseDirectory;
//            var path = basePath + "\\";
//            container.Install(FromAssembly.InDirectory(new AssemblyFilter(path, "Business.dll")));

//        }

//        public static void Dispose()
//        {
//            //container.Dispose();
//        }

//        static void Kernel_ComponentRegistered(string key, Castle.MicroKernel.IHandler handler)
//        {
//            //Intercept all methods of classes those have at least one method that has Transactional attribute.
//            foreach (var method in handler.ComponentModel.Implementation.GetMethods())
//            {
//                if (Reflection.ReflectionHelper.HasMethodTheAttribute<TransactionalAttribute>(method))
//                {
//                    handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(Aop.TransactionalInterceptor)));
//                    return;
//                }


//                if (Reflection.ReflectionHelper.HasMethodTheAttribute<BroadcasterAttribute>(method))
//                {
//                    handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(Aop.BroadcasterInterceptor)));
//                    return;
//                }

//            }
//        }
//    }
//}
