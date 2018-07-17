
//using Castle.DynamicProxy;
//using Framework.Attribute;
using System;
using System.Linq;

namespace Ybm.Infrastructure.Core.Aop
{

    //public class BroadcasterInterceptor : IInterceptor
    //{
    //    public void Intercept(IInvocation invocation)
    //    {
    //        if (!invocation.MethodInvocationTarget.GetCustomAttributes(typeof(BroadcasterAttribute), false).Any())
    //            invocation.Proceed();
    //        else
    //            try
    //            {
    //                var methodInfo = invocation.MethodInvocationTarget;
    //                var attribute =  methodInfo.CustomAttributes.FirstOrDefault();
    //                var type =  (Type)attribute.ConstructorArguments[0].Value;
    //                var methodName = (string)attribute.ConstructorArguments[1].Value;
    //                var runFirst = (bool)attribute.ConstructorArguments[2].Value;

    //                object instance = ServiceFactory.CreateInstance(type);

    //                if (runFirst)
    //                {
    //                    instance.GetType().GetMethod(methodName).Invoke(instance, null);
    //                    invocation.Proceed();
    //                }
    //                else
    //                {
    //                    invocation.Proceed();
    //                    instance.GetType().GetMethod(methodName).Invoke(instance, null);
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                throw new AggregateException(ex);
    //            }
    //    }
    //}
}
