using System.Collections.Generic;

namespace ONS.WEBPMO.Application.DTO.DTO
{
    public class DadosPesquisaColetaInsumoDTO
    {
        public DadosPesquisaColetaInsumoDTO()
        {
            Agentes = new List<ChaveDescricaoDTO<int>>();
            Insumos = new List<ChaveDescricaoDTO<int>>();
            SituacoesColeta = new List<ChaveDescricaoDTO<int>>();
            SemanasOperativas = new List<ChaveDescricaoDTO<int>>();
        }

        public string NomeSemanaOperativaSituacao { get; set; }
        public bool IsSemanaOperativaEmConfiguracao { get; set; }
        public string VersaoStringSemanaOperativa { get; set; }
        public IList<ChaveDescricaoDTO<int>> SemanasOperativas { get; set; }
        public IList<ChaveDescricaoDTO<int>> SituacoesColeta { get; set; }
        public IList<ChaveDescricaoDTO<int>> Agentes { get; set; }
        public IList<ChaveDescricaoDTO<int>> Insumos { get; set; }
    }
}
