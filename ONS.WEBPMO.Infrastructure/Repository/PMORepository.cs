using AspNetCore.IQueryable.Extensions;
using Microsoft.EntityFrameworkCore;
using ONS.WEBPMO.Domain.Entities.Base;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository.PMO;
using ONS.WEBPMO.Infrastructure.Context;
using ONS.WEBPMO.Infrastructure.DataBase;

namespace ONS.WEBPMO.Domain.Repositories.Impl
{
    public class PMORepository : Repository<PMO>, IPMORepository
    {
        public PMORepository(WEBPMODbContext context) : base(context)
        {
        }

        public PMO ObterPorFiltro(IBaseFilter filtro)
        {
            var query = Query.AsQueryable().AsNoTracking()
                    .Include(x => x.SemanasOperativas)
                        .Apply(filtro);

            return query.FirstOrDefault();


        }

        public PMO ObterPorFiltro(PMOFilter filtro)
        {
            throw new NotImplementedException();
        }

        public PMO ObterPorFiltroExterno(PMOFilter filtro)
        {
            var query = Query.AsQueryable();

            if (filtro.Ano.HasValue)
            {
                query = query.Where(p => p.AnoReferencia == filtro.Ano.Value);
            }

            if (filtro.Mes.HasValue)
            {
                query = query.Where(p => p.MesReferencia == filtro.Mes.Value);
            }

            return query.AsNoTracking().FirstOrDefault();
        }

        public int ObterQuantidadeSemanasPMO(int idSemanaOperativa)
        {
            throw new NotImplementedException();
        }
    }
}
