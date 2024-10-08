using ONS.Common.Util.Pagination;
using ONS.WEBPMO.Domain.DTO;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository;
using ONS.WEBPMO.Infrastructure.Context;
using ONS.WEBPMO.Infrastructure.DataBase;

namespace ONS.WEBPMO.Domain.Repositories.Impl
{
    public class DadoColetaManutencaoRepository : Repository<DadoColetaManutencao>, IDadoColetaManutencaoRepository
    {
        public DadoColetaManutencaoRepository(WEBPMODbContext context) : base(context)
        {
        }

        public bool Any(int idColetaInsumo, string idOrigemColeta, DateTime dataInicio, DateTime dataFim, int? idDadoColetaDiferente = null)
        {
            throw new NotImplementedException();
        }

        public bool AnyWithTime(int idColetaInsumo, string idOrigemColeta, DateTime dataInicio, DateTime dataFim, int? idDadoColetaDiferente = null)
        {
            throw new NotImplementedException();
        }

        public IList<DadoColetaManutencao> ConsultarDadosComInsumoParticipaBlocoMP(int idSemanaOperativa)
        {
            throw new NotImplementedException();
        }

        public PagedResult<DadoColetaManutencaoDTO> ConsultarPorColetaInsumo(DadoColetaInsumoFilter idColetaInsumo)
        {
            throw new NotImplementedException();
        }

        public void DeletarPorIdGabarito(IList<int> idsGabarito)
        {
            throw new NotImplementedException();
        }

        public DadoColetaManutencao FindByColetaInsumoId(int coletaInsumoId)
        {
            throw new NotImplementedException();
        }

        public DadoColetaManutencao FindByKey(int chave)
        {
            throw new NotImplementedException();
        }
    }
}
