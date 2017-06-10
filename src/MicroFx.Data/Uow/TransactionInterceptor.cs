using System;
using System.Diagnostics;
using Castle.DynamicProxy;

namespace MicroFx.Data.Uow
{
    [DebuggerStepThrough]
    public class TransactionInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine("Transaction intercepted");
            var atts = invocation.MethodInvocationTarget.GetCustomAttributes(typeof(TransactionAttribute), true);

            if (atts.Length > 0)
            {
                With.Transaction(invocation.Proceed, ((TransactionAttribute)atts[0]).IsolationLevel);
            }
            else
            {
                With.Transaction(invocation.Proceed);
            }
        }
    }
}