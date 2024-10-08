using log4net;
using ons.common.providers.schemas;
using ONS.Common.Entities.Pop;
using ONS.Common.Seguranca;
using ONS.Common.Services.Impl;
using ONS.Common.Util.Email;
using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Enumerations;
using System.Configuration;
using System.Net.Mail;


namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    public class NotificacaoService : Service, INotificacaoService
    {
        private readonly static ILog log = LogManager.GetLogger(typeof(INotificacaoService));

        public void NotificarUsuariosPorAgente(int idAgente, string assunto, string mensagem)
        {
            var usuarios = UserInfo.ConsultarUsuariosRepresentamAgente(idAgente, "Agente");
            var emails = GetEmails(usuarios);
            EnviarEmail(assunto, mensagem, emails.ToArray());
        }

        public void NotificarUsuariosPorAgentes(IList<int> idsAgente, string assunto, string mensagem)
        {
            IEnumerable<string> emails = new HashSet<string>();

            IList<POPUser> usuarios = new List<POPUser>();

            foreach (var idAgente in idsAgente)
            {
                usuarios.Union(UserInfo.ConsultarUsuariosRepresentamAgente(idAgente, "Agente"));
            }

            var emailsPrincipais = GetEmails(usuarios);
            emails = emails.Union(emailsPrincipais);

            EnviarEmail(assunto, mensagem, emails.Distinct().ToArray());
        }

        public void NotificarUsuariosPorAgentesList(IList<int> idsAgente, string assunto, string mensagem)
        {
            IEnumerable<string> emails = new HashSet<string>();

            List<POPUser> usuarios = new List<POPUser>();

            var usuariosPorAgentes = UserInfo.ConsultarUsuariosRepresentamAgentes(idsAgente.ToArray(), "Agente");

            usuarios.AddRange(usuariosPorAgentes);

            var emailsPrincipais = GetEmails(usuarios);
            emails = emails.Union(emailsPrincipais);

            EnviarEmail(assunto, mensagem, emails.Distinct().ToArray());
        }


        public void NotificarUsuariosPorPerfil(RolePermissoesPopEnum perfil, string assunto, string mensagem)
        {
            List<UserInfoEscopoFilter> escoposFiltro = escoposFiltro = new List<UserInfoEscopoFilter>();
            if (perfil == RolePermissoesPopEnum.RepresentanteCCEE)
            {
                escoposFiltro.Add(new UserInfoEscopoFilter { Escopo = UserInfoEscopoFilter.EscopoEnum.CCEE, TipoEscopo = UserInfoEscopoFilter.TipoEscopoEnum.AGENTES });
                escoposFiltro.Add(new UserInfoEscopoFilter { Escopo = UserInfoEscopoFilter.EscopoEnum.CCEE, TipoEscopo = UserInfoEscopoFilter.TipoEscopoEnum.INST });
            }
            else
            {
                escoposFiltro.Add(new UserInfoEscopoFilter { Escopo = UserInfoEscopoFilter.EscopoEnum.ONS, TipoEscopo = UserInfoEscopoFilter.TipoEscopoEnum.ONS });
            }

            var usuarios = UserInfo.ConsultarUsuariosPorPerfil(perfil.ToDescription(), escoposFiltro);
            var emails = GetEmails(usuarios.ToArray());

            EnviarEmail(assunto, mensagem, emails.Distinct().ToArray());
        }

        private IList<string> GetEmails(IList<POPUser> usuarios)
        {
            IList<string> emails = new List<string>();

            foreach (var usuario in usuarios)
            {
                if (usuario != null)
                {
                    if (!string.IsNullOrWhiteSpace(usuario.Email))
                    {
                        emails.Add(usuario.Email);
                    }

                    log.InfoFormat("Notificação: Usuário '{0}' | Email '{1}'.", usuario, usuario.Email);
                }
            }
            return emails;
        }

        private void EnviarEmail(string assunto, string mensagem, params string[] destinatarios)
        {
            EmailService emailService = new EmailService();
            MailMessage mailMessage = new MailMessage();

            mailMessage.SubjectEncoding = System.Text.Encoding.Unicode;

            //Incluido pela demanda de edição de mensagens.
            mailMessage.IsBodyHtml = true;

            string emailTo = ConfigurationManager.AppSettings["email.sgipmo"];

            mailMessage.To.Add(new MailAddress(emailTo));
            foreach (string destinatario in destinatarios.Distinct())
            {
                if (!destinatario.Contains(".tsintegre"))
                {
                    if (!destinatario.Contains(".hsintegre"))
                    {
                        mailMessage.Bcc.Add(new MailAddress(destinatario));
                    }
                }
            }

            log.InfoFormat("Destinatário '{0}' notificado. {1}", string.Join(",", destinatarios), assunto);

            mailMessage.Body = mensagem;
            mailMessage.Subject = assunto;

            emailService.SendMail(mailMessage);
        }

        public List<EmpresasASeremNotificadas> ConverteListaUserxAgeEmAgexUsuers(List<POPUser> usuarios)
        {
            List<EmpresasASeremNotificadas> listaEmpresas = new List<EmpresasASeremNotificadas>();
            foreach (POPUser usuario in usuarios)
            {
                foreach (EscopoDTO escopo in usuario.EscopoLista)
                {
                    EmpresasASeremNotificadas empresa = new EmpresasASeremNotificadas();
                    empresa.Nome = escopo.NomeEscopo;
                    empresa.NomeLongo = escopo.NomeEscopo;
                    empresa.Id = int.Parse(escopo.IdEscopo);
                    POPUser us = new POPUser();
                    us.Nome = usuario.Nome;
                    us.Email = usuario.Email;
                    us.Login = usuario.Login;
                    us.EscopoLista = usuario.EscopoLista;
                    empresa.ListaUsuarios = new List<POPUser>();
                    empresa.ListaUsuarios.Add(us);
                    listaEmpresas.Add(empresa);
                }
            }

            List<EmpresasASeremNotificadas> listaEmpresasDistinct = new List<EmpresasASeremNotificadas>();

            foreach (var empresa in listaEmpresas)
            {
                if (!listaEmpresasDistinct.Any(x => x.Id == empresa.Id))
                {
                    listaEmpresasDistinct.Add(empresa);
                }
                else
                {
                    listaEmpresasDistinct.Where(x => x.Id == empresa.Id).First().ListaUsuarios.AddRange(empresa.ListaUsuarios.ToArray());
                }
            }
            return listaEmpresasDistinct;
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
        public List<POPUser> ListaUsuarios { get; set; }
        public virtual ISet<ColetaInsumo> ColetasInsumos { get; set; }
        public virtual ISet<Gabarito> Gabaritos { get; set; }
        //public virtual ISet<LogNotificacao> LogNotificacoes { get; set; }
    }

}
