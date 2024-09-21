namespace ONS.WEBPMO.Application.Services.PMO.Interfaces
{
    [ServiceContract]
    public interface ILogNotificacaoService : IService
    {
        [OperationContract]
        [UseNetDataContractSerializer("Agente", "SemanaOperativa")]
        PagedResult<LogNotificacao> ObterLogNotificacao(LogNotificacaoFilter filter);

        void LogarNotificacao(SemanaOperativa semanaOperativa, List<Agente> agentes, DateTime? dataHoraAcao, string nomeUsuario, string acao);

        [OperationContract]
        bool Apagar(List<int> idsLogs);
    }
}
