using log4net;
using Microsoft.Extensions.Configuration;
using ONS.WEBPMO.Application.DTO;
using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Enumerations;
using System.Net.Mail;


namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    public class NotificacaoService : INotificacaoService
    {
        private readonly static ILog log = LogManager.GetLogger(typeof(INotificacaoService));

        public void NotificarUsuariosPorAgente(int idAgente, string assunto, string mensagem)
        {
            throw new NotImplementedException();
        }

        public void NotificarUsuariosPorAgentes(IList<int> idsAgente, string assunto, string mensagem)
        {
            throw new NotImplementedException();
        }

        public void NotificarUsuariosPorAgentesList(IList<int> idsAgente, string assunto, string mensagem)
        {
            throw new NotImplementedException();
        }

        public void NotificarUsuariosPorPerfil(RolePermissoesPopEnum perfil, string assunto, string mensagem)
        {
            throw new NotImplementedException();
        }
    }

    public class UsuariosASeremNotificado
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public List<EscopoDTO> Empresas { get; set; }

    }

    public class EmpresasASeremNotificadas
    {
        public EmpresasASeremNotificadas()
        {
            ColetasInsumos = new HashSet<ColetaInsumo>();
            Gabaritos = new HashSet<Gabarito>();
            //this.LogNotificacoes = new HashSet<LogNotificacao>();
        }

        public string Nome { get; set; }
        public string NomeLongo { get; set; }
        public int Id { get; set; }
        //public List<POPUser> ListaUsuarios { get; set; }
        public virtual ISet<ColetaInsumo> ColetasInsumos { get; set; }
        public virtual ISet<Gabarito> Gabaritos { get; set; }
        //public virtual ISet<LogNotificacao> LogNotificacoes { get; set; }
    }

}
