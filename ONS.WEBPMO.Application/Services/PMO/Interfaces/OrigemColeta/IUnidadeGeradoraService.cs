namespace ONS.WEBPMO.Application.Services.PMO.Interfaces.OrigemColeta
{
    [ServiceContract]
    public interface IUnidadeGeradoraService : IService
    {
        [OperationContract]
        [UseNetDataContractSerializer]
        UnidadeGeradora ObterUnidadeGeradoraPorChave(int chave);

        [OperationContract]
        [UseNetDataContractSerializer]
        IList<UnidadeGeradora> ConsultarUnidadesGeradorasPorChaves(params int[] chaves);

        [OperationContract]
        [UseNetDataContractSerializer]
        IList<UnidadeGeradora> ConsultarUnidadesGeradorasPorNome(string nome);
    }
}
