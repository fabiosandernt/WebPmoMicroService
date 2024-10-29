using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Application.Services.PMO.Interfaces
{
    //[ServiceContract]
    public interface IDadoColetaEstruturadoService
    {


        DadoColetaEstruturado ObterPorChave(int chave);
    }
}
