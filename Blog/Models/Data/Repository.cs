using System;
using System.Linq;
using System.Linq.Expressions;
using AJ.UtiliTools;
using BC.Data.Context;

namespace BC.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected IDataContextFactory _dataContextFactory;

        public virtual T GetByID(int id)
        {
            var itemParameter = Expression.Parameter(typeof(T), "item");
            var whereExpression = Expression.Lambda<Func<T, bool>>(Expression.Equal(Expression.Property(itemParameter, PrimaryKeyName), Expression.Constant(id)), new[] { itemParameter });

            return All().Where(whereExpression).SingleOrDefault();
        }

        /// <summary>
        /// Return all instances of type T.
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> All()
        {
            return GetTable;
        }

        /// <summary>
        /// Return all instances of type T that match the expression exp.
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public SearchResult<T> FindAll(Func<T, bool> exp, int? PageSize, int? Page, OrderByGroup orderByGroup)
        {
            if (PageSize.HasValue && Page.HasValue)
            {
                var pagedExpression = GetTable.Where<T>(exp).AsQueryable().Order(orderByGroup);

                SearchResult<T> returnVal = new SearchResult<T>(pagedExpression.Count(), PageSize.Value, Page.Value, pagedExpression.Skip((Page.Value - 1) * PageSize.Value).Take(PageSize.Value).AsQueryable());

                return returnVal;
            }

            return new SearchResult<T>(GetTable.Where<T>(exp).AsQueryable().Order(orderByGroup));
        }

        public SearchResult<T> FindAll_Predicate(Expression<Func<T, bool>> predicate, int? PageSize, int? Page, OrderByGroup orderByGroup)
        {
            if (PageSize.HasValue && Page.HasValue)
            {
                var pagedExpression = GetTable.Where<T>(predicate).Order(orderByGroup);

                SearchResult<T> returnVal = new SearchResult<T>(pagedExpression.Count(), PageSize.Value, Page.Value, pagedExpression.Skip((Page.Value - 1) * PageSize.Value).Take(PageSize.Value).AsQueryable());

                return returnVal;
            }

            return new SearchResult<T>(GetTable.Where<T>(predicate).Order(orderByGroup));
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return GetTable.Where<T>(predicate).Count();
        }

        /// <summary>See IRepository.</summary>
        /// <param name="exp"></param><returns></returns>
        public T Single(Func<T, bool> exp)
        {
            return GetTable.Single(exp);
        }

        /// <summary>See IRepository.</summary>
        /// <param name="exp"></param><returns></returns>
        public T First(Func<T, bool> exp)
        {
            return GetTable.FirstOrDefault(exp);
        }

        public T First_Predicate(Expression<Func<T, bool>> predicate)
        {
            return GetTable.Where<T>(predicate).FirstOrDefault();
        }

        /// <summary>See IRepository.</summary>
        /// <param name="entity"></param>
        public virtual void MarkForDeletion(T entity)
        {
            _dataContextFactory.Context.GetTable<T>().DeleteOnSubmit(entity);
        }

        public T Insert(T entity)
        {
            _dataContextFactory.Context.GetTable<T>().InsertOnSubmit(entity);

            return entity;
        }

        public T Update(T entity)
        {
            GetTable.Attach(entity, true);
            //IInvoiceData data = (IInvoiceData)entity;
            //MarkForDeletion(GetByID(data.ID()));
            //Insert(entity);

            return entity;
        }

        /// <summary>
        /// Create a new instance of type T.
        /// </summary>
        /// <returns></returns>
        public virtual T CreateInstance()
        {
            T entity = Activator.CreateInstance<T>();
            GetTable.InsertOnSubmit(entity);
            return entity;
        }

        /// <summary>See IRepository.</summary>
        public void SaveAll()
        {
            _dataContextFactory.SaveAll();
        }

        public Repository(IDataContextFactory dataContextFactory)
        {
            _dataContextFactory = dataContextFactory;
        }

        #region Properties

        private string PrimaryKeyName
        {
            get { return TableMetadata.RowType.IdentityMembers[0].Name; }
        }

        private System.Data.Linq.Table<T> GetTable
        {
            get { return _dataContextFactory.Context.GetTable<T>(); }
        }

        private System.Data.Linq.Mapping.MetaTable TableMetadata
        {
            get { return _dataContextFactory.Context.Mapping.GetTable(typeof(T)); }
        }

        private System.Data.Linq.Mapping.MetaType ClassMetadata
        {
            get { return _dataContextFactory.Context.Mapping.GetMetaType(typeof(T)); }
        }

        #endregion
    }
}