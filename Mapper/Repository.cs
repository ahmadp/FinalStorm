using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mapper
{
    public class Repository<T> where T : class
    {

        private FinalStromContext dataContext;
        internal DbSet<T> dbSet;

        public Repository(FinalStromContext dataContext)
        {
            this.dataContext = dataContext;
            this.dbSet = dataContext.Set<T>();
        }
        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, string orderBy = "", string thenBy = "", string includeProperties = "")
        {
            IQueryable<T> dbset = dbSet;
            var query = GetQuery<T>(dbset, filter, orderBy, thenBy, includeProperties);
            var list = query.ToList();
            return list;
        }
        public virtual IEnumerable<T> GetAll()
        {
            return this.DataSource();
        }

        public virtual T GetById(int id)
        {
            return this.dataContext.Set<T>().Find(id);
        }
        public virtual T GetById(long id)
        {
            return this.dataContext.Set<T>().Find(id);
        }
        public virtual T GetById(Guid id)
        {
            return this.dataContext.Set<T>().Find(id);
        }

        public virtual void Insert(T entity)
        {
            this.dataContext.Set<T>().Add(entity);
            this.SaveChanges();
        }


        public virtual void Update(T entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            dataContext.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void UpdateOnly(T entityToUpdate, string propertyNames)
        {
            dbSet.Attach(entityToUpdate);
            foreach (var propertyName in propertyNames.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                dataContext.Entry(entityToUpdate).Property(propertyName).IsModified = true;
            }
        }

        public virtual void UpdateWithout(T entityToUpdate, string propertyNames)
        {
            dbSet.Attach(entityToUpdate);
            dataContext.Entry(entityToUpdate).State = EntityState.Modified;
            foreach (var propertyName in propertyNames.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                dataContext.Entry(entityToUpdate).Property(propertyName).IsModified = false;
            }
        }

        public virtual void Delete(T entity)
        {
            if (dataContext.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            this.dataContext.Set<T>().Remove(entity);
            this.SaveChanges();
        }


        #region Private Helpers
        internal IQueryable<VEntity> GetQuery<VEntity>(IQueryable<VEntity> query, Expression<Func<VEntity, bool>> filter, string orderBy, string thenBy, string includeProperties)
        {
            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (!string.IsNullOrEmpty(orderBy))
            {
                IOrderedQueryable<VEntity> orderedQuery = null;

                string[] orderByExpr = orderBy.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (orderByExpr.Length == 1 || orderByExpr[1].Equals("ASC"))
                {
                    orderedQuery = query.OrderBy<VEntity>(orderByExpr[0]);
                }
                else if (orderByExpr[1].Equals("DESC"))
                {
                    orderedQuery = query.OrderByDescending<VEntity>(orderByExpr[0]);
                }
                else
                {
                    throw new ArgumentException("Order Direction is not valid");
                }

                if (!string.IsNullOrEmpty(thenBy))
                {
                    foreach (var thenByProperty in thenBy.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        string[] thenByExpr = thenByProperty.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (thenByExpr.Length == 1 || thenByExpr[1].Equals("ASC"))
                        {
                            orderedQuery = orderedQuery.ThenBy<VEntity>(thenByExpr[0]);
                        }
                        else if (thenByExpr[1].Equals("DESC"))
                        {
                            orderedQuery = orderedQuery.ThenByDescending<VEntity>(thenByExpr[0]);
                        }
                        else
                        {
                            throw new ArgumentException("Order Direction is not valid");
                        }
                    }
                }

                return orderedQuery;
            }

            return query;
        }

        /// <summary>
        /// Returns expression to use in expression trees, like where statements. For example query.Where(GetExpression("IsDeleted", typeof(boolean), false));
        /// </summary>
        /// <param name="propertyName">The name of the property. Either boolean or a nulleable typ</param>
        private Expression<Func<T, bool>> GetExpression(string propertyName, object value)
        {
            var param = Expression.Parameter(typeof(T));
            var actualValueExpression = Expression.Property(param, propertyName);

            var lambda = Expression.Lambda<Func<T, bool>>(
                Expression.Equal(actualValueExpression,
                    Expression.Constant(value)),
                param);

            return lambda;
        }

        protected IQueryable<T> DataSource()
        {
            var query = dataContext.Set<T>().AsQueryable<T>();
            var property = typeof(T).GetProperty("Deleted");

            if (property != null)
            {
                query = query.Where(GetExpression("Deleted", null));
            }

            return query;
        }

        protected virtual void SaveChanges()
        {
            this.dataContext.SaveChanges();
        }
        #endregion
    }
}
