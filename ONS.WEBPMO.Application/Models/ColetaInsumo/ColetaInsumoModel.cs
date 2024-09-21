using System.ComponentModel.DataAnnotations;


namespace ONS.WEBPMO.Application.Models.ColetaInsumo
{
    public class ColetaInsumoModel : FiltroPesquisaColetaInsumoModel
    {
        public int IdColetaInsumo { get; set; }
        public SituacaoColetaInsumoEnum SituacaoColetaInsumo { get; set; }
        public string SituacaoColetaInsumoDescricao { get; set; }
        public string VersaoString { get; set; }
        public byte[] Versao { get; set; }
        public string Situacao { get; set; }
        public SituacaoSemanaOperativaEnum SituacaoSemanaOperativa { get; set; }

        public int IdAgente { get; set; }
        public string NomeAgente { get; set; }
        public string CodigoPerfilONS { get; set; }

        public int IdInsumo { get; set; }
        public string NomeInsumo { get; set; }

        [StringLength(800, ErrorMessage = "Campo 'Motivo de Alteração' deve possuir no máximo 800 caracteres.")]
        public string MotivoAlteracaoONS { get; set; }

        [StringLength(800, ErrorMessage = "Campo 'Motivo de Rejeição' deve possuir no máximo 800 caracteres.")]
        public string MotivoRejeicaoONS { get; set; }

        public bool IsSemanaPmo { get; set; }
        public bool IsGeracaoComplementar { get; set; }

        public string SituacaoSemanaOperativaDescricao
        {
            get
            {
                return SituacaoSemanaOperativa.ToDescription();
            }
        }

        public string NomeESituacaoSemanaOperativa
        {
            get
            {
                return string.Format("{0} - {1}", NomeSemanaOperativa, SituacaoSemanaOperativa.ToDescription());
            }
        }

        public bool IsInsumoParaDECOMP { get; set; }

        public bool VisualizarBotaoIncluirManutencao { get; set; } = false;

        public string NomesGrandezasNaoEstagioAlteradas { get; set; }

        public bool EhInsumoVolumesIniciais { get { return !string.IsNullOrEmpty(NomeInsumo) && NomeInsumo.Trim().ToLower() == "volumes iniciais"; } }

    }
}