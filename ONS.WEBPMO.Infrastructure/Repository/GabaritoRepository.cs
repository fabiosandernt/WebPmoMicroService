
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository.PMO;
using ONS.WEBPMO.Infrastructure.Context;
using ONS.WEBPMO.Infrastructure.DataBase;

namespace ONS.WEBPMO.Domain.Repositories.Impl
{
    public class GabaritoRepository : Repository<Gabarito>, IGabaritoRepository
    {
        public GabaritoRepository(WEBPMODbContext context) : base(context)
        {
        }


        public IList<Gabarito> ConsultarGabaritoParticipaBloco(int idSemanaOperativa)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Gabarito> ConsultarParaRemover(IList<int> ids)
        {
            throw new NotImplementedException();
        }

        public IList<Gabarito> ConsultarPorGabaritoFilter(GabaritoConfiguracaoFilter filter)
        {
            throw new NotImplementedException();
        }

        public ICollection<Gabarito> ConsultarPorGabaritoFilterPaginado(GabaritoConfiguracaoFilter filter)
        {
            throw new NotImplementedException();
        }




        public void DeletarPorIdSemanaOperativa(int idSemanaOperativa)
        {
            throw new NotImplementedException();
        }

        public Gabarito ObterPorColetaInumoOrigemColeta(int idColetaInsumo, string idOrigemColeta)
        {
            throw new NotImplementedException();
        }
    }
}
