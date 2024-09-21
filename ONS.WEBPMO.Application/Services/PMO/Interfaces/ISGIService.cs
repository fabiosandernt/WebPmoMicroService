namespace ONS.WEBPMO.Application.Services.PMO.Interfaces
{
    [ServiceContract]
    public interface ISGIService : IService
    {
        [OperationContract]
        [UseNetDataContractSerializer]
        IList<DadoColetaManutencao> ObterManutencoesPorChaves(string[] chavesUnidadesGeradoras,
            DateTime dataInicio, DateTime dataFim);
    }
}
