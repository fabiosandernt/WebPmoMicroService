namespace ONS.WEBPMO.Application.Services.PMO.Interfaces
{
    [ServiceContract]
    public interface IAgenteService : IService
    {
        [OperationContract]
        [UseNetDataContractSerializer]
        Agente ObterOuCriarAgentePorChave(int chave);

        [OperationContract]
        [UseNetDataContractSerializer]
        Agente ObterAgentePorChaveOnline(int idAgente);

        [OperationContract]
        [UseNetDataContractSerializer]
        IList<Agente> ConsultarAgentesPorNomeOnline(string nome);

        [OperationContract]
        [UseNetDataContractSerializer]
        IList<Agente> ConsultarTodosAgentesPorNomeOnline(string nome);

        [OperationContract]
        [UseNetDataContractSerializer]
        PagedResult<Agente> ConsultarAgentesParticipamGabaritoPaginado(GabaritoParticipantesFilter filter);

        [OperationContract]
        [UseNetDataContractSerializer]
        IList<Agente> ConsultarAgentesParticipamGabarito(GabaritoParticipantesFilter filter);

        [OperationContract]
        [UseNetDataContractSerializer]
        IList<Agente> ConsultarAgentesParticipanteGabaritoRepresentadoUsuarioLogado(int? idSemanaOperativa);

        [OperationContract]
        [UseNetDataContractSerializer]
        bool IsAgenteONS(int idAgente);

        [OperationContract]
        [UseNetDataContractSerializer]
        IList<Agente> ConsultarAgentesPorNome(string nomeAgente);

        [TransactionRequired]
        void SincronizarAgentesComCDRE();

        [OperationContract]
        [UseNetDataContractSerializer]
        List<Agente> ObterAgentesPorIds(IList<int> idsAgente);
    }
}
