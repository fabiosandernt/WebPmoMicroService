
namespace ONS.WEBPMO.Domain.Entities.Usina
{

    public class Insumo : BaseObject
    {
        private string nome;
        private string siglainsumo;

        public Insumo()
        {
            ColetasInsumo = new HashSet<ColetaInsumo>();
            Gabaritos = new HashSet<Gabarito>();
            DataUltimaAtualizacao = DateTime.Now;
        }

        public string Nome
        {
            get { return nome; }
            set { nome = value != null ? value.Trim() : null; }
        }

        public short OrdemExibicao { get; set; }
        public bool PreAprovado { get; set; }
        public bool Reservado { get; set; }
        public string TipoInsumo { get; set; }
        public DateTime? DataUltimaAtualizacao { get; set; }
        public byte[] Versao { get; set; }

        public virtual ISet<ColetaInsumo> ColetasInsumo { get; set; }
        public virtual ISet<Gabarito> Gabaritos { get; set; }

        public object Version
        {
            get { return Versao; }
        }

        public string VersaoStringInsumo
        {
            get
            {
                if (Versao != null)
                {
                    return Convert.ToBase64String(Versao);
                }
                return string.Empty;
            }
        }

        public override string ToString()
        {
            return this.Nome;
        }

        public string SiglaInsumo
        {
            get { return siglainsumo; }
            set { siglainsumo = value != null ? value.Trim() : null; }
        }

        public bool ExportarInsumo { get; set; }
    }
}
