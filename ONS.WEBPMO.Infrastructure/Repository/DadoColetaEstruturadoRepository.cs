using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository;
using ONS.WEBPMO.Infrastructure.Context;
using ONS.WEBPMO.Infrastructure.DataBase;

namespace ONS.WEBPMO.Domain.Repositories.Impl
{
    public class DadoColetaEstruturadoRepository : Repository<DadoColetaEstruturado>, IDadoColetaEstruturadoRepository
    {
        public DadoColetaEstruturadoRepository(WEBPMODbContext context) : base(context)
        {
        }

        public IList<DadoColetaEstruturado> ConsultarDadosComInsumoEGrandezaParticipaBloco(int idSemanaOperativa)
        {
            throw new NotImplementedException();
        }

        public IList<DadoColetaEstruturado> ConsultarDadosComInsumoEGrandezaParticipaBlocoGNL(int idSemanaOperativa)
        {
            throw new NotImplementedException();
        }

        public IList<DadoColetaEstruturado> ConsultarPorFiltro(DadoColetaInsumoFilter filter)
        {
            throw new NotImplementedException();
        }

        public int ContarQuantidadeLinhasDadosEstruturados(int idColetaInsumo)
        {
            throw new NotImplementedException();
        }

        public void DeletarPorIdGabarito(IList<int> idsGabarito)
        {
            throw new NotImplementedException();
        }
    }
}
