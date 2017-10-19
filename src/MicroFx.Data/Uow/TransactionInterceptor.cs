using System;
using System.Diagnostics;
using Castle.DynamicProxy;
using log4net;

namespace MicroFx.Data.Uow
{
    [DebuggerStepThrough]
    public class TransactionInterceptor : IInterceptor
    {
        private ILog logger = LogManager.GetLogger(typeof (TransactionInterceptor));

        public void Intercept(IInvocation invocation)
        {
            logger.Info("Transaction intercepted");

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