using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EFCore.DataAccess;
using EFCore.Entities;

namespace EFCore.Business
{
    public class BusinessBase<TEntity> where TEntity : EntityBase
    {
        public List<TEntity> QueryAll()
        {
            using (var efr = new EFHelper<OracleContext>())
            {
                var list = efr.QueryAll<TEntity>().ToList();
                return list;
            }
        }

        public List<TEntity> QueryExp(Expression<Func<TEntity, bool>> exp)
        {
            using (var efr = new EFHelper<OracleContext>())
            {
                var list = efr.QueryExp<TEntity>(exp).ToList();
                return list;
            }
        }
    }
}
