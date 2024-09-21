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

namespace ONS.WEBPMO.Domain.Repositories.Impl
{
    public class TipoColetaRepository : Repository<TipoColeta>, ITipoColetaRepository
    {
        public override IList<TipoColeta> All()
        {
            return base.All().Where(c => c.UsoPmo).ToList();
        }
        public override IList<TipoColeta> All(Func<IQueryable<TipoColeta>, IOrderedQueryable<TipoColeta>> orderBy)
        {
            return base.All(orderBy).Where(c => c.UsoPmo).ToList();
        }
    }
}
