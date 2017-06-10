using System;
using System.Transactions;
using log4net;
using IsolationLevel = System.Data.IsolationLevel;

namespace MicroFx.Data.Uow
{
    public static class With
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(With));

        public static void Transaction(Action transactional, IsolationLevel level = IsolationLevel.ReadUncommitted)
        {
            var unitOfWork = IoC.Resolve<IUnitOfWork>();
           
            // If we are already in a transaction, don't start a new one
            if (unitOfWork.InTransaction)
            {
                logger.Info("Enlisting in existing transaction");
                transactional();
            }
            else
            {
                logger.Info("Starting new transaction");
                using (var ts = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = GetSystemTranIsolationLevel(level) }))
                {
                    using (var tx = unitOfWork.CreateTransaction(level))
                    {
                        try
                        {

                            transactional();

                            tx.Commit();

                            logger.Info("transaction committed");

                        }
                        catch (Exception ex)
                        {
                            try
                            {
                                tx.Rollback();
                                logger.Info("transaction rollbacked");
                            }
                            catch (Exception rollbackException)
                            {
                                logger.Error("With.Transaction rollback exception.", rollbackException);
                                logger.Error("With.Transaction exception before rollback.", ex);
                            }

                            throw;
                        }
                    }

                    ts.Complete();
                }
            }
        }


        private static System.Transactions.IsolationLevel GetSystemTranIsolationLevel(IsolationLevel level)
        {
            switch (level)
            {
                case IsolationLevel.ReadCommitted:
                    return System.Transactions.IsolationLevel.ReadCommitted;
                case IsolationLevel.Chaos:
                    return System.Transactions.IsolationLevel.Chaos;
                case IsolationLevel.ReadUncommitted:
                    return System.Transactions.IsolationLevel.ReadUncommitted;
                case IsolationLevel.RepeatableRead:
                    return System.Transactions.IsolationLevel.RepeatableRead;
                case IsolationLevel.Serializable:
                    return System.Transactions.IsolationLevel.Serializable;
                case IsolationLevel.Snapshot:
                    return System.Transactions.IsolationLevel.Snapshot;
                default:
                    return System.Transactions.IsolationLevel.Unspecified;
            }
        }

    }
}