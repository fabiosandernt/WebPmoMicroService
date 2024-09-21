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
    public class TipoDadoGrandezaRepository : Repository<TipoDadoGrandeza>, ITipoDadoGrandezaRepository
    {
        public override IList<TipoDadoGrandeza> All()
        {
            return base.All().Where(c => c.UsoPmo).ToList();
        }
        public override IList<TipoDadoGrandeza> All(Func<IQueryable<TipoDadoGrandeza>, IOrderedQueryable<TipoDadoGrandeza>> orderBy)
        {
            return base.All(orderBy).Where(c => c.UsoPmo).ToList();
        }
    }
}
