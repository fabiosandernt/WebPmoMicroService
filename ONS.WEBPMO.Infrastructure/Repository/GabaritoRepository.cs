using ONS.Common.Util.Pagination;
using ONS.WEBPMO.Domain.DTO;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository;
using ONS.WEBPMO.Infrastructure.Context;
using ONS.WEBPMO.Infrastructure.DataBase;

namespace ONS.WEBPMO.Domain.Repositories.Impl
{
    public class GabaritoRepository : Repository<Gabarito>, IGabaritoRepository
    {
        public GabaritoRepository(WEBPMODbContext context) : base(context)
        {
        }

        public IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoGeracaoComplementar>> ConsultarAgentesComGeracaoComplementar(bool isPadrao, string nomeRevisao = "")
        {
            throw new NotImplementedException();
        }

        public IList<GabaritoParticipantesBaseDTO<AgenteParticipanteGabaritoDTO>> ConsultarAgentesParticipantesGabarito(bool isPadrao, string nomeRevisao = "")
        {
            throw new NotImplementedException();
        }

        public IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoInsumoNaoEstruturadoDTO>> ConsultarAgentesPodemEnviarDadosNaoEstruturadosPorGabarito(bool isPadrao, string nomeRevisao = "")
        {
            throw new NotImplementedException();
        }

        public PagedResult<GabaritoAgrupadoAgenteOrigemColetaDTO> ConsultarAgrupadoPorAgenteTipoOrigemPaginado(GabaritoOrigemColetaFilter filter)
        {
            throw new NotImplementedException();
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

        public PagedResult<Gabarito> ConsultarPorGabaritoFilterPaginado(GabaritoConfiguracaoFilter filter)
        {
            throw new NotImplementedException();
        }

        public IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoReservatorioDTO>> ConsultarReservatorioPorGabarito(bool isPadrao, string nomeRevisao = "")
        {
            throw new NotImplementedException();
        }

        public IList<GabaritoParticipantesBaseDTO<ReservatorioParticipanteGabaritoDTO>> ConsultarReservatoriosParticipantesGabarito(bool isPadrao, string nomeRevisao = "")
        {
            throw new NotImplementedException();
        }

        public IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoSubsistemaDTO>> ConsultarSubsistemaPorGabarito(bool isPadrao, string nomeRevisao = "")
        {
            throw new NotImplementedException();
        }

        public IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoUGEDTO>> ConsultarUGEPorGabarito(bool isPadrao, string nomeRevisao = "")
        {
            throw new NotImplementedException();
        }

        public IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoUsinaDTO>> ConsultarUsinaPorGabarito(bool isPadrao, string nomeRevisao = "")
        {
            throw new NotImplementedException();
        }

        public IList<GabaritoParticipantesBaseDTO<UsinaParticipanteGabaritoDTO>> ConsultarUsinasParticipantesGabarito(bool isPadrao, string nomeRevisao = "")
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
