namespace ONS.WEBPMO.Domain.Presentations
{
    [ServiceContract]
    public interface ILogNotificacaoPresentation : IService
    {
        [OperationContract]
        [UseNetDataContractSerializer]
        LogNotificacaoDTO ObterDadosPesquisaLogNotificacao(int? idSemanaOperativa = null, bool isMonitorar = false, bool? ordernarListagens = true);
    }
}
