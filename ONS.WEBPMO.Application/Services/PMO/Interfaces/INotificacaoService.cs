using ONS.WEBPMO.Domain.Enumerations;

namespace ONS.WEBPMO.Application.Services.PMO.Interfaces
{
    //[ServiceContract]
    public interface INotificacaoService 
    {
       
        
        void NotificarUsuariosPorAgente(int idAgente, string assunto, string mensagem);

        
        
        
        void NotificarUsuariosPorAgentes(IList<int> idsAgente, string assunto, string mensagem);
       
        
        void NotificarUsuariosPorAgentesList(IList<int> idsAgente, string assunto, string mensagem);

        
        
        
        void NotificarUsuariosPorPerfil(RolePermissoesPopEnum perfil, string assunto, string mensagem);
    }
}
