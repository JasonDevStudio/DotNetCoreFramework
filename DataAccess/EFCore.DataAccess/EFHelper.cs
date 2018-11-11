using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using Commmon.Utility.Enums;

namespace EFCore.DataAccess
{
    public class EFHelper<T> :IDisposable where T : DbContext, new()
    {
        private DbContext dbcont;

        public EFHelper()
        {
            this.dbcont = new T();
        }

        public EFHelper(T obj)
        {
            this.dbcont = obj;
        }

        #region Query

        /// <summary>
        /// Queries the exp.
        /// </summary>
        /// <returns>The exp.</returns>
        /// <param name="exp">Exp.</param>
        /// <typeparam name="TEntity">The 1st type parameter.</typeparam>
        public List<TEntity> QueryExp<TEntity>(Expression<Func<TEntity, bool>> exp) where TEntity : class
        {
            var query = dbcont.Query<TEntity>().Where(exp);
            return query.ToList();
        }

        #endregion

        #region Update

        /// <summary>
        /// Bulks the insert.
        /// </summary>
        /// <returns>The insert.</returns>
        /// <param name="entities">Entities.</param>
        /// <typeparam name="TEntity">The 1st type parameter.</typeparam>
        public (EnumMethodState state,string msg) BulkInsert<TEntity>(List<TEntity> entities) where TEntity :class
        {
            try
            {
                dbcont.BulkInsert(entities); 
                return (EnumMethodState.Success, string.Empty);
            }
            catch (Exception ex)
            {
                return (EnumMethodState.Error, ex.ToString());
            }
        }

        public void Dispose()
        {

        }

        #endregion
    }
}
