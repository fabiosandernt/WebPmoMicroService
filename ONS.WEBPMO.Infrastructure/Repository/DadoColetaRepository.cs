using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository;
using ONS.WEBPMO.Infrastructure.Context;
using ONS.WEBPMO.Infrastructure.DataBase;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Repositories
{
    public class DadoColetaRepository : Repository<DadoColeta>, IDadoColetaRepository
    {
        public DadoColetaRepository(WEBPMODbContext context) : base(context)
        {
        }

        public IList<DadoColeta> ConsultarDadosComInsumoParticipaBlocoUH(int idSemanaOperativa)
        {
            throw new NotImplementedException();
        }

        public IList<DadoColeta> ConsutarParaGeracaoBlocos(int idSemanaOperativa)
        {
            throw new NotImplementedException();
        }

        public void DeletarPorIdGabarito(IList<int> idsGabarito)
        {
            throw new NotImplementedException();
        }
    }
}
