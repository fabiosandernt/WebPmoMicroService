using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Application.Services.PMO.Interfaces
{
    //[ServiceContract]
    public interface IGrandezaService 
    {
        
        
        Grandeza ObterPorChave(int chave);
    }
}
