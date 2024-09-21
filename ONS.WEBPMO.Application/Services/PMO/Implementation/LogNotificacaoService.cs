using ONS.WEBPMO.Application.Services.PMO.Interfaces;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    public class LogNotificacaoService : Service, ILogNotificacaoService
    {
        public const string LOG_NOTIFICACAO_ABERTURA = "Abertura";
        public const string LOG_NOTIFICACAO_REABERTURA = "Reabertura";
        public const string LOG_NOTIFICACAO_REJEICAO = "Rejeição";

        private readonly ILogNotificacaoRepository logNotificacaoRepository;
        public LogNotificacaoService(ILogNotificacaoRepository logNotificacaoRepository)
        {
            this.logNotificacaoRepository = logNotificacaoRepository;
        }

        public PagedResult<LogNotificacao> ObterLogNotificacao(LogNotificacaoFilter filter)
        {
            PagedResult<LogNotificacao> logsNotificacao = new PagedResult<LogNotificacao>(new List<LogNotificacao>(), 0, 0, 0);

            logsNotificacao = logNotificacaoRepository.ObterLogNotificacao(filter);

            return logsNotificacao;
        }

        public void LogarNotificacao(SemanaOperativa semanaOperativa, List<Agente> agentes, DateTime? dataHoraAcao, string nomeUsuario, string acao)
        {

            var idsListaAgentes = agentes.Select(a => a.Id).ToArray();
            var usuarios = UserInfo.ConsultarUsuariosRepresentamAgentes(idsListaAgentes, "Agente");

            var empresasASeremNotificadas = ConverteListaUserxAgeEmAgexUsuers(usuarios.ToList());

            foreach (var agente in agentes)
            {

                var usuariosDoAgente = empresasASeremNotificadas.FirstOrDefault(emp => emp.Id == agente.Id);

                if (usuariosDoAgente != null && usuariosDoAgente.ListaUsuarios != null)
                {
                    var emails = string.Join("; ", usuariosDoAgente.ListaUsuarios.Select(usuario => usuario.Email));
                    LogNotificacao logNotificacao = new LogNotificacao();
                    logNotificacao.SemanaOperativa = semanaOperativa;
                    logNotificacao.Agente = agente;
                    logNotificacao.Acao = acao;
                    logNotificacao.DataEnvioNotificacao = dataHoraAcao.Value;
                    logNotificacao.EmailsEnviar = emails;
                    logNotificacao.EmailsEnviados = emails;
                    logNotificacao.Usuario = nomeUsuario;
                    logNotificacaoRepository.Add(logNotificacao);
                }

            }

        }

        public bool Apagar(List<int> idsLogs)
        {
            foreach (int id in idsLogs)
            {
                logNotificacaoRepository.DeleteByKey(id);
            }

            logNotificacaoRepository.SaveChanges();

            return true;
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

}
