using System;
using System.Collections.Generic;
using System.Linq;

namespace MicroFx.Data
{
    /// <summary>
    /// Base interface to implement Repository Pattern
    /// </summary>
    public interface IRepository<T> where T : class
    {
       
        /// <summary>
        /// Add item into repository
        /// </summary>
        /// <param name="item">Item to add to repository</param>
        void Save(T item);

        /// <summary>
        /// Delete item 
        /// </summary>
        /// <param name="item">Item to delete</param>
        void Delete(T item);

        /// <summary>
        /// Set item as modified
        /// </summary>
        /// <param name="item">Item to modify</param>
        void Update(T item);

       
        /// <summary>
        /// Get element by entity key
        /// </summary>
        /// <param name="id">Entity key value</param>
        /// <returns></returns>
        T Get(int id);

        /// <summary>
        /// Get all elements of type T in repository
        /// </summary>
        /// <returns>List of selected elements</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Returns IQueryable of Entities.
        /// </summary>
        /// <returns>IQueryable of T</returns>
        IQueryable<T> Query();
    }

    public interface IDbRegisteration
    {
        Type GetRepositoryType();
        Type GetUowType();
    }
}