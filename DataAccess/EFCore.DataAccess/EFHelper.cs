using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using Commmon.Utility.Enums;

namespace EFCore.DataAccess
{
    public class EFHelper<T> : IDisposable where T : DbContext, new()
    {
        private DbContext dbcont;

        public EFHelper()
        {
            this.dbcont = new T();
            Z.EntityFramework.Extensions.LicenseManager.AddLicense("1638;100-Huawei", "77248321-6103-24d6-9a64-cbd2c71f7077");
        }

        public EFHelper(T obj)
        {
            this.dbcont = obj;
            Z.EntityFramework.Extensions.LicenseManager.AddLicense("1638;100-Huawei", "77248321-6103-24d6-9a64-cbd2c71f7077");
        }

        #region Query

        /// <summary>
        /// Queries the all.
        /// </summary>
        /// <returns>The exp.</returns> 
        /// <typeparam name="TEntity">The 1st type parameter.</typeparam>
        public IQueryable<TEntity> QueryAll<TEntity>() where TEntity : class => dbcont.Query<TEntity>();

        /// <summary>
        /// Queries the exp.
        /// </summary>
        /// <returns>The exp.</returns>
        /// <param name="exp">Exp.</param>
        /// <typeparam name="TEntity">The 1st type parameter.</typeparam>
        public IQueryable<TEntity> QueryExp<TEntity>(Expression<Func<TEntity, bool>> exp) where TEntity : class => QueryAll<TEntity>();

        #endregion

        #region Update

        /// <summary>
        /// Bulks the insert.
        /// </summary>
        /// <returns>The insert.</returns>
        /// <param name="entities">Entities.</param>
        /// <typeparam name="TEntity">The 1st type parameter.</typeparam>
        public (EnumMethodState state, string msg) BulkInsert<TEntity>(List<TEntity> entities) where TEntity : class
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
