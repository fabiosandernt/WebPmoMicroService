namespace ONS.WEBPMO.Application.Services.PMO.Interfaces
{
    [ServiceContract]
    public interface ILogDadosInformadosService : IService
    {
        [OperationContract]
        [UseNetDataContractSerializer("Agente", "SemanaOperativa")]
        PagedResult<LogDadosInformados> obterLogDadosInformados(LogDadosInformadosFilter filter);

    }
}
