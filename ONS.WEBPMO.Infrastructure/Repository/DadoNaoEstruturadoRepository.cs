using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository.PMO;
using ONS.WEBPMO.Infrastructure.Context;
using ONS.WEBPMO.Infrastructure.DataBase;

namespace ONS.WEBPMO.Domain.Repositories.Impl
{
    public class DadoColetaNaoEstruturadoRepository : Repository<DadoColetaNaoEstruturado>, IDadoColetaNaoEstruturadoRepository
    {
        public DadoColetaNaoEstruturadoRepository(WEBPMODbContext context) : base(context)
        {
        }

        public void DeletarPorIdGabarito(IList<int> idsGabarito)
        {
            throw new NotImplementedException();
        }

        public DadoColetaNaoEstruturado ObterDadoColetaNaoEstruturado(DadoColetaInsumoNaoEstruturadoFilter filtros)
        {
            throw new NotImplementedException();
        }

        public IList<DadoColetaNaoEstruturado> ObterDadosColetaNaoEstruturado(ArquivosSemanaOperativaFilter filtro)
        {
            throw new NotImplementedException();
        }
    }
}
