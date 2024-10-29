using ONS.WEBPMO.Domain.Entities.Base;

namespace ONS.WEBPMO.Domain.Entities.PMO
{
    public class Agente : BaseEntity<int>
    {
        public Agente()
        {
            ColetasInsumos = new HashSet<ColetaInsumo>();
            Gabaritos = new HashSet<Gabarito>();
            LogNotificacoes = new HashSet<LogNotificacao>();
        }

        public string NomeLongo { get; set; }
        public string Nome { get; set; }

        public virtual ISet<ColetaInsumo> ColetasInsumos { get; set; }
        public virtual ISet<Gabarito> Gabaritos { get; set; }
        public virtual ISet<LogNotificacao> LogNotificacoes { get; set; }
        public virtual ISet<LogDadosInformados> LogDadosInformados { get; set; }
    }
}
