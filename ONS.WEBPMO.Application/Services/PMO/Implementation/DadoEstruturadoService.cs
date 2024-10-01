using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    public class DadoColetaEstruturadoService :  IDadoColetaEstruturadoService
    {
        private IDadoColetaEstruturadoRepository DadoColetaEstruturadoRepository { get; set; }

        public DadoColetaEstruturadoService(IDadoColetaEstruturadoRepository DadoColetaEstruturadoRepository)
        {
            this.DadoColetaEstruturadoRepository = DadoColetaEstruturadoRepository;
        }

        public DadoColetaEstruturado ObterPorChave(int chave)
        {
            return DadoColetaEstruturadoRepository.FindByKey(chave);
        }
    }
}
