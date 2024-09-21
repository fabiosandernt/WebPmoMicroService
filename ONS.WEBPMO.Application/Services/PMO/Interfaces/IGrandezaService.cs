namespace ONS.WEBPMO.Application.Services.PMO.Interfaces
{
    [ServiceContract]
    public interface IGrandezaService : IService
    {
        [OperationContract]
        [UseNetDataContractSerializer]
        Grandeza ObterPorChave(int chave);
    }
}
