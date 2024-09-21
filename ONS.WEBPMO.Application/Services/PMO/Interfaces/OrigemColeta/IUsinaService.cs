namespace ONS.WEBPMO.Application.Services.PMO.Interfaces.OrigemColeta
{
    [ServiceContract]
    public interface IUsinaService : IService
    {
        [OperationContract]
        [UseNetDataContractSerializer]
        Usina ObterUsinaPorChave(int chave);

        [OperationContract]
        [UseNetDataContractSerializer]
        IList<Usina> ConsultarUsinasPorChaves(params int[] chaves);

        [OperationContract]
        [UseNetDataContractSerializer]
        IList<Usina> ConsultarUsinasPorNome(string nome);
    }
}
