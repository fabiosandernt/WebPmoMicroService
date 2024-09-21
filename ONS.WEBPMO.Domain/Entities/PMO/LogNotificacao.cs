using ONS.WEBPMO.Domain.Entities.Base;

namespace ONS.WEBPMO.Domain.Entities.PMO
{
    public class LogNotificacao : BaseEntity<int>
    {
        public virtual SemanaOperativa SemanaOperativa { get; set; }
        public int SemanaOperativaId { get; set; }
        public virtual Agente Agente { get; set; }
        public int AgenteId { get; set; }
        public virtual string Usuario { get; set; }
        public virtual string Acao { get; set; }
        public virtual DateTime DataEnvioNotificacao { get; set; }
        public virtual string EmailsEnviar { get; set; }
        public virtual string EmailsEnviados { get; set; }

        public string NomeAgentePerfil
        {
            get
            {
                return Agente != null ? Agente.Nome : "Nome agente não encontrado";
            }
        }

        public string NomeSemanaOperativa
        {
            get
            {
                return SemanaOperativa != null ? SemanaOperativa.Nome : "Nome semana operativa não encontrado";
            }
        }
    }
}
