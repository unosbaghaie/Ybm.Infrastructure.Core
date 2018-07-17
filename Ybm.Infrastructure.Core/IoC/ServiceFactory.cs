//using System;

//namespace Ybm.Infrastructure.Core
//{
//    public class ServiceFactory
//    {
//        public static T CreateInstance<T>(string name = null) 
//        {
//            var instance = name == null ?
//                Framework.ContainerManager.Container.Resolve<T>() :
//                Framework.ContainerManager.Container.Resolve<T>(name);

//            //Subscribe all instance's methods to their evnets
//            //new SubscribeEngine().RegisterEvents(instance);
//            //new ServiceEventEngine().Fire(instance);

//            return instance;
//        }
//        public static void Release(object instance)
//        {
//            Framework.ContainerManager.Container.Release(instance);
//        }
//        public static object CreateInstance(Type type)
//        {
//            var instance = Framework.ContainerManager.Container.Resolve(type);
//            return instance;
//        }
//    }
//}

