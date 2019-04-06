using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using Commmon.Utility.Enums;
using EFCore.Entities;

namespace EFCore.DataAccess
{
    public class EFHelper<T> : IDisposable where T : DbContext, new()
    {
        /// <summary>
        /// DbContext
        /// </summary>
        public DbContext DBCont { get; private set; }

        public EFHelper()
        {
            this.DBCont = new T();
            Z.EntityFramework.Extensions.LicenseManager.AddLicense("1638;100-Huawei", "77248321-6103-24d6-9a64-cbd2c71f7077");
        }

        public EFHelper(T obj)
        {
            this.DBCont = obj;
            Z.EntityFramework.Extensions.LicenseManager.AddLicense("1638;100-Huawei", "77248321-6103-24d6-9a64-cbd2c71f7077");
        }

        #region Query


        /// <summary>
        /// Queries the all.
        /// </summary>
        /// <returns>The exp.</returns> 
        /// <typeparam name="TEntity">The 1st type parameter.</typeparam>
        public IQueryable<TEntity> QueryAll<TEntity>() where TEntity : class => this.DBCont.Set<TEntity>();

        /// <summary>
        /// Queries the exp.
        /// </summary>
        /// <returns>The exp.</returns>
        /// <param name="exp">Exp.</param>
        /// <typeparam name="TEntity">The 1st type parameter.</typeparam>
        public IQueryable<TEntity> QueryExp<TEntity>(Expression<Func<TEntity, bool>> exp) where TEntity : EntityBase => QueryAll<TEntity>().Where(exp);

        #endregion

        #region Update

        /// <summary>
        /// Bulks the insert.
        /// </summary>
        /// <returns>The insert.</returns>
        /// <param name="entities">Entities.</param>
        /// <typeparam name="TEntity">The 1st type parameter.</typeparam>
        public (EnumMethodState state, string msg) BulkInsert<TEntity>(List<TEntity> entities, string seq = null) where TEntity : EntityBase
        {
            try
            {
                DBCont.BulkInsert(entities, opt =>
                {
                   //opt.CustomProvider = new Z.BulkOperations.CustomProvider(Z.BulkOperations.ProviderType.OracleDevArt); 
                });
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
