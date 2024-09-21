namespace ONS.WEBPMO.Application.Services.PMO.Interfaces
{
    [ServiceContract]
    public interface INotificacaoService : IService
    {
        [Async]
        [OperationContract]
        [UseNetDataContractSerializer]
        void NotificarUsuariosPorAgente(int idAgente, string assunto, string mensagem);

        [Async]
        [OperationContract]
        [UseNetDataContractSerializer]
        void NotificarUsuariosPorAgentes(IList<int> idsAgente, string assunto, string mensagem);
        [Async]
        [OperationContract]
        [UseNetDataContractSerializer]
        void NotificarUsuariosPorAgentesList(IList<int> idsAgente, string assunto, string mensagem);

        [Async]
        [OperationContract]
        [UseNetDataContractSerializer]
        void NotificarUsuariosPorPerfil(RolePermissoesPopEnum perfil, string assunto, string mensagem);
    }
}
