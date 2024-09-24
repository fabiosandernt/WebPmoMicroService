
using ONS.WEBPMO.Domain.Entities.Usina.OrigemColetaUsina;

namespace ONS.WEBPMO.Domain.Entities.Usina
{
    public class Gabarito : BaseObject
    {
        public Gabarito()
        {
            DadosColeta = new List<DadoColeta>();
        }
        public bool IsPadrao { get; set; }
        public string CodigoPerfilONS { get; set; }
        public byte[] Versao { get; set; }

        public virtual IList<DadoColeta> DadosColeta { get; set; }
        public virtual Insumo Insumo { get; set; }
        public virtual OrigemColeta OrigemColeta { get; set; }
        public virtual SemanaOperativa SemanaOperativa { get; set; }
        public virtual Agente Agente { get; set; }

        public string NomeAgentePerfil
        {
            get
            {
                return string.IsNullOrWhiteSpace(CodigoPerfilONS)
                    ? Agente.Nome
                    : string.Format("{0}/{1}", Agente.Nome, CodigoPerfilONS);
            }
        }

        public int AgenteId { get; set; }
        public int InsumoId { get; set; }
        public string OrigemColetaId { get; set; }
        public int? SemanaOperativaId { get; set; }

    }
}
