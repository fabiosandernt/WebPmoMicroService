using ONS.WEBPMO.Application.DTO;
using ONS.WEBPMO.Application.Services.PMO.Interfaces.Integrations;
using ONS.WEBPMO.Application.Services.PMO.Interfaces.OrigemColeta;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO;
using ONS.WEBPMO.Domain.Enumerations;
using ONS.WEBPMO.Domain.Repository;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    public class OrigemColetaService : IOrigemColetaService
    {
        private readonly IOrigemColetaRepository origemColetaRepository;
        private readonly IBDTPMOService BDTService;

        public OrigemColetaService(
            IOrigemColetaRepository origemColetaRepository,
            IBDTPMOService BDTService)
        {
            this.origemColetaRepository = origemColetaRepository;
            this.BDTService = BDTService;
        }

        public IList<OrigemColeta> ConsultarOrigemColetaPorTipoNomeOnline(TipoOrigemColetaEnum tipoOrigemColeta, string nome)
        {
            throw new NotImplementedException();
        }

        public ICollection<OrigemColeta> ConsultarOrigemColetasGabaritoPaginado(GabaritoParticipantesFilter filter)
        {
            throw new NotImplementedException();
        }

        public IList<OrigemColeta> ConsultarOuCriarOrigemColetaPorIds(IList<string> idsOrigemColeta, TipoOrigemColetaEnum tipoOrigemColeta)
        {
            throw new NotImplementedException();
        }

        public IList<UnidadeGeradora> ConsultarUnidadeGeradoParticipanteGabaritoPorColetaInsumo(int idColetaInsumo)
        {
            throw new NotImplementedException();
        }

        public IList<UnidadeGeradoraManutencaoSGIDTO> ConsultarUnidadeGeradoParticipanteGabaritoPorColetaInsumos(List<int> idColetaInsumos)
        {
            throw new NotImplementedException();
        }

        public IList<UnidadeGeradora> ConsultarUnidadeGeradoParticipanteGabaritoPorColetaInsumoUsina(int idColetaInsumo, string idUsina)
        {
            throw new NotImplementedException();
        }

        public IList<UnidadeGeradora> ConsultarUnidadeGeradoraPorUsinaOnline(string idOrigemColeta)
        {
            throw new NotImplementedException();
        }

        public IList<Usina> ConsultarUsinaParticipanteGabaritoPorColetaInsumo(int idColetaInsumo)
        {
            throw new NotImplementedException();
        }

        public T ObterOrigemColetaPorChave<T>(string id) where T : OrigemColeta
        {
            throw new NotImplementedException();
        }

        public T ObterOrigemColetaPorChaveOnline<T>(string id) where T : OrigemColeta
        {
            throw new NotImplementedException();
        }

        public OrigemColeta ObterOuCriarOrigemColetaPorId(string idOrigemColeta, TipoOrigemColetaEnum tipoOrigemColeta)
        {
            throw new NotImplementedException();
        }

        public void SincronizarOrigensColetaComBDT()
        {
            throw new NotImplementedException();
        }
    }
}
