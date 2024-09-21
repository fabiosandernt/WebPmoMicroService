using ONS.WEBPMO.Domain.Entities.Base;

namespace ONS.WEBPMO.Domain.Entities.PMO
{
    public class ColetaInsumo : BaseEntity<int>, IConcurrencyEntity
    {
        public ColetaInsumo()
        {
            DadosColeta = new List<DadoColeta>();
        }

        public string MotivoAlteracaoONS { get; set; }
        public string MotivoRejeicaoONS { get; set; }
        public byte[] Versao { get; set; }

        public string CodigoPerfilONS { get; set; }
        public DateTime DataHoraAtualizacao { get; set; }
        public string LoginAgenteAlteracao { get; set; }

        public int? SituacaoId { get; set; }
        public virtual SituacaoColetaInsumo Situacao { get; set; }
        public virtual IList<DadoColeta> DadosColeta { get; set; }
        public virtual Insumo Insumo { get; set; }
        public virtual SemanaOperativa SemanaOperativa { get; set; }
        public virtual Agente Agente { get; set; }

        public object Version
        {
            get
            {
                return Versao;
            }
        }

        public string NomeAgentePerfil
        {
            get
            {
                return string.IsNullOrEmpty(CodigoPerfilONS)
                    ? Agente.Nome
                    : string.Format("{0}/{1}", Agente.Nome, CodigoPerfilONS);
            }
        }

        public string VersaoString
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

        public bool IsInsumoDECOMP
        {
            get
            {
                InsumoNaoEstruturado insumoNaoEstruturado = Insumo as InsumoNaoEstruturado;
                if (insumoNaoEstruturado != null)
                {
                    return insumoNaoEstruturado.IsUtilizadoDECOMP;
                }
                return false;
            }
        }

        public int AgenteId { get; set; }
        public int InsumoId { get; set; }
        public int SemanaOperativaId { get; set; }
        public string NomesGrandezasNaoEstagioAlteradas { get; set; }
    }
}
