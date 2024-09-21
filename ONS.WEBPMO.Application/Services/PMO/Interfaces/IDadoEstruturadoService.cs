namespace ONS.WEBPMO.Application.Services.PMO.Interfaces
{
    [ServiceContract]
    public interface IDadoColetaEstruturadoService : IService
    {
        [OperationContract]
        [UseNetDataContractSerializer]
        DadoColetaEstruturado ObterPorChave(int chave);
    }
}
