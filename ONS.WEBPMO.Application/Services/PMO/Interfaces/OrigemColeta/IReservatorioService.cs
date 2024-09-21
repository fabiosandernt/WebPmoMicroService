namespace ONS.WEBPMO.Application.Services.PMO.Interfaces.OrigemColeta
{
    [ServiceContract]
    public interface IReservatorioService : IService
    {
        [OperationContract]
        [UseNetDataContractSerializer]
        Reservatorio ObterReservatorioPorChave(int chave);

        [OperationContract]
        [UseNetDataContractSerializer]
        IList<Reservatorio> ConsultarReservatoriosPorChaves(params int[] chaves);

        [OperationContract]
        [UseNetDataContractSerializer]
        IList<Reservatorio> ConsultarReservatoriosPorNome(string nome);
    }
}
