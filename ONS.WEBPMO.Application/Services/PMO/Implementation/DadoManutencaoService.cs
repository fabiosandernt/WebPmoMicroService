using ONS.WEBPMO.Application.DTO;
using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository.PMO;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    public class DadoColetaManutencaoService : IDadoColetaManutencaoService
    {
        private readonly IDadoColetaManutencaoRepository dadoColetaManutencaoRepository;

        public DadoColetaManutencaoService(IDadoColetaManutencaoRepository dadoColetaEstruturadoRepository)
        {
            dadoColetaManutencaoRepository = dadoColetaEstruturadoRepository;
        }

        public void AlterarDadoColeta(DadoColetaManutencao dadoColeta)
        {
            throw new NotImplementedException();
        }

        public ICollection<DadoColetaManutencaoDTO> ConsultarDadoColetaManutencaoPorColetaInsumo(DadoColetaInsumoFilter filter)
        {
            throw new NotImplementedException();
        }

        public void Excluir(DadoColetaManutencao dadoColeta)
        {
            throw new NotImplementedException();
        }

        public void IncluirDadoColeta(DadoColetaManutencao dadoColeta)
        {
            throw new NotImplementedException();
        }

        public void IncluirDadoColetaSeNaoExiste(IList<DadoColetaManutencao> dadoColetaList)
        {
            throw new NotImplementedException();
        }

        public DadoColetaManutencao ObterPorChave(int chave)
        {
            throw new NotImplementedException();
        }

        public DadoColetaManutencao ObterPorColetaInsumoId(int idColetaInsumo)
        {
            throw new NotImplementedException();
        }
    }
}
