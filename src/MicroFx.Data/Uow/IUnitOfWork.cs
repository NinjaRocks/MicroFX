using System;
using System.Data;
using System.Data.Common;

namespace MicroFx.Data.Uow
{
    /// <summary>
    /// Base interface to implement UnitOfWork Pattern.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Commit all changes made in a container.
        /// </summary>
        ///<remarks>
        /// If the entity have fixed properties and any optimistic concurrency problem exists,  
        /// then an exception is thrown
        ///</remarks>
        void Commit();

       /// <summary>
        /// Rollback tracked changes. See references of UnitOfWork pattern
        /// </summary>
        void Rollback();

        /// <summary>
        /// Creates a new transaction with default isolation level
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        DbTransaction CreateTransaction(IsolationLevel level);

        /// <summary>
        /// Indicates if already in a transaction
        /// </summary>
        bool InTransaction { get; }
    }
}