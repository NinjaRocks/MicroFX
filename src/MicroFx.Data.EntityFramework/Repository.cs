using System;
using System.Collections.Generic;
using System.Linq;
using MicroFx.Data.Uow;

namespace MicroFx.Data.EntityFramework
{
    public class Repository<T> : IRepository<T> where T : class
    {

        #region Members

        readonly IQueryableUnitOfWork unitOfWork;

        #endregion

        #region Constructor

        /// <summary>
        /// Create a new instance of repository
        /// </summary>
        /// <param name="unitOfWork">Associated Unit Of Work</param>
        public Repository(IQueryableUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException("unitOfWork");

            this.unitOfWork = unitOfWork;
        }

        #endregion

        #region IRepository Members

        /// <summary>
        /// 
        /// </summary>
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return unitOfWork;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public virtual void Save(T item)
        {

            if (item != null)
                unitOfWork.SetAdded(item); // add new item in this set


            unitOfWork.Commit();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public virtual void Delete(T item)
        {
            if (item != null)
            {
                //attach item if not exist
                unitOfWork.Attach(item);

                //set as "removed"
                unitOfWork.SetDeleted(item);

                unitOfWork.Commit();
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public virtual void TrackItem(T item)
        {
            if (item != null)
                unitOfWork.Attach(item);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public virtual void Update(T item)
        {
            if (item != null)
            {
                unitOfWork.SetModified(item);
                unitOfWork.Commit();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="persisted"></param>
        /// <param name="current"></param>
        public virtual void Merge(T persisted, T current)
        {
            unitOfWork.ApplyCurrentValues(persisted, current);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T Get(int id)
        {
            if (id != 0)
                return unitOfWork.CreateSet<T>().Find(id);

            return null;
        }

        /// <summary>
        /// Get all elements of type T in repository
        /// </summary>
        /// <returns>List of selected elements</returns>
        public virtual IEnumerable<T> GetAll()
        {
            return unitOfWork.CreateSet<T>();
        }

        /// <summary>
        /// Returns IQueryable of Entities.
        /// </summary>
        /// <returns>IQueryable of T</returns>
        public virtual IQueryable<T> Query()
        {
            return unitOfWork.CreateSet<T>();
        }

        #endregion
    }
}