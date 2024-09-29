using ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO;

namespace ONS.WEBPMO.Application.Services.PMO.Interfaces.OrigemColeta
{
    //[ServiceContract]
    public interface IReservatorioService 
    {
        
        
        Reservatorio ObterReservatorioPorChave(int chave);

        
        
        IList<Reservatorio> ConsultarReservatoriosPorChaves(params int[] chaves);

        
        
        IList<Reservatorio> ConsultarReservatoriosPorNome(string nome);
    }
}
