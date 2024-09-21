namespace ONS.WEBPMO.Application.Services.PMO.Interfaces
{
    [ServiceContract]
    public interface IDadoColetaNaoEstruturadoService : IService
    {
        [OperationContract]
        [UseNetDataContractSerializer]
        DadoColetaNaoEstruturado ObterPorChave(int chave);
    }
}
