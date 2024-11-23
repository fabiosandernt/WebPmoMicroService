using ONS.WEBPMO.Domain.Entities.Base;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository;
using ONS.WEBPMO.Infrastructure.Context;
using ONS.WEBPMO.Infrastructure.DataBase;
using AspNetCore.IQueryable.Extensions;
using Microsoft.EntityFrameworkCore;

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
                 .ThenInclude(x => x.ColetasInsumos)
                 .Apply(filtro);

            return query.FirstOrDefault();


        }

        public PMO ObterPorFiltroExterno(PMOFilter filtro)
        {
            throw new NotImplementedException();
        }

        public int ObterQuantidadeSemanasPMO(int idSemanaOperativa)
        {
            throw new NotImplementedException();
        }
    }
}
