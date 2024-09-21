using System.Collections.Generic;

namespace ONS.WEBPMO.Application.DTO
{
    public class DadoColetaNaoEstruturadoDTO
    {
        public DadoColetaNaoEstruturadoDTO()
        {
            this.Arquivos = new HashSet<ArquivoDadoNaoEstruturadoDTO>();
        }

        public int IdColetaInsumo { get; set; }
        public byte[] VersaoColetaInsumo { get; set; }
        public int IdGabarito { get; set; }
        public int IdSituacaoColetaInsumo { get; set; }

        public int IdSemanaOperativa { get; set; }
        public string NomeSemanaOperativa { get; set; }

        public string DescricaoSituacaoSemanaOperativa { get; set; }
        public int IdSituacaoSemanaOperativa { get; set; }

        public int IdInsumo { get; set; }
        public string NomeInsumo { get; set; }

        public int IdAgente { get; set; }
        public string NomeAgente { get; set; }

        public int IdDadoColetaInsumo { get; set; }
        public string MotivoAlteracaoONS { get; set; }
        public string MotivoRejeicaoONS { get; set; }
        public string Observacao { get; set; }
        public ISet<ArquivoDadoNaoEstruturadoDTO> Arquivos { get; set; }
        public bool IsInsumoParaDECOMP { get; set; }

    }
}
