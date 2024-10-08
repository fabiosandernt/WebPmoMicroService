using ONS.Common.Util.Pagination;
using ONS.WEBPMO.Domain.DTO;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository.Base;

namespace ONS.WEBPMO.Domain.Repository
{
    public interface IDadoColetaManutencaoRepository : IRepository<DadoColetaManutencao>
    {
        DadoColetaManutencao FindByKey(int chave);
        DadoColetaManutencao FindByColetaInsumoId(int coletaInsumoId);
        PagedResult<DadoColetaManutencaoDTO> ConsultarPorColetaInsumo(DadoColetaInsumoFilter idColetaInsumo);
        bool Any(int idColetaInsumo, string idOrigemColeta, DateTime dataInicio, DateTime dataFim, int? idDadoColetaDiferente = null);
        void DeletarPorIdGabarito(IList<int> idsGabarito);
        IList<DadoColetaManutencao> ConsultarDadosComInsumoParticipaBlocoMP(int idSemanaOperativa);

        bool AnyWithTime(int idColetaInsumo, string idOrigemColeta, DateTime dataInicio, DateTime dataFim, int? idDadoColetaDiferente = null);

    }
}
