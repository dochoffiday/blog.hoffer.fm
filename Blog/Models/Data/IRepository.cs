using System;
using System.Linq;
using System.Linq.Expressions;
using AJ.UtiliTools;

namespace BC.Data
{
    public interface IRepository<T> where T : class
    {
        T GetByID(int id);

        /// <summary>
        /// Return all instances of type T.
        /// </summary>
        /// <returns></returns>
        IQueryable<T> All();

        /// <summary>
        /// Return all instances of type T that match the expression exp.
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        SearchResult<T> FindAll(Func<T, bool> exp, int? PageSize, int? Page, OrderByGroup orderByGroup);

        SearchResult<T> FindAll_Predicate(Expression<Func<T, bool>> predicate, int? PageSize, int? Page, OrderByGroup orderByGroup);

        int Count(Expression<Func<T, bool>> predicate);

        /// <summary>Returns the single entity matching the expression.
        /// Throws an exception if there is not exactly one such entity.</summary>
        /// <param name="exp"></param><returns></returns>
        T Single(Func<T, bool> exp);

        /// <summary>Returns the first element satisfying the condition.</summary>
        /// <param name="exp"></param><returns></returns>
        T First(Func<T, bool> exp);

        T First_Predicate(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Mark an entity to be deleted when the context is saved.
        /// </summary>
        /// <param name="entity"></param>
        void MarkForDeletion(T entity);

        T Insert(T entity);

        T Update(T entity);

        /// <summary>
        /// Create a new instance of type T.
        /// </summary>
        /// <returns></returns>
        T CreateInstance();

        /// <summary>Persist the data context.</summary>
        void SaveAll();
    }
}