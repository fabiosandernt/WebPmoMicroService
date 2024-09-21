using System.Data.Entity;
using System.Data.SqlClient;
using System.Security.Cryptography;
using ONS.Common.Repositories.Impl;
using ONS.SGIPMO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ONS.Common.Util.Pagination;
using System.Collections;
namespace ONS.WEBPMO.Domain.Repositories.Impl
{
    public class CategoriaInsumoRepository : Repository<CategoriaInsumo>, ICategoriaInsumoRepository
    {
        public override IList<CategoriaInsumo> All() {
            return base.All().Where( c => c.UsoPmo ).ToList();
        }
        
        public override IList<CategoriaInsumo> All(Func<IQueryable<CategoriaInsumo>, IOrderedQueryable<CategoriaInsumo>> orderBy)
        {
            return base.All(orderBy).Where(c => c.UsoPmo).ToList();
        }
    }
}
