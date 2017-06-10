using System;
using System.Data;

namespace MicroFx.Data.Uow
{
    [AttributeUsage(AttributeTargets.Method)]
    public class TransactionAttribute : Attribute
    {
        private IsolationLevel isolationLevel = IsolationLevel.ReadCommitted;
        
        public IsolationLevel IsolationLevel
        {
            get { return isolationLevel; }
            set { isolationLevel = value; }
        }
    }
}