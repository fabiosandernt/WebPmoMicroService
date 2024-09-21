
using ONS.WEBPMO.Domain.Enumerations;

namespace ONS.WEBPMO.Application.DTO
{
    public class ValorDadoColetaDTO : DadoColetaDTOBase
    {
        public ValorDadoColetaDTO()
        {

        }

        public ValorDadoColetaDTO(DadoColetaDTO dadoColetaDto)
        {
            AceitaValorNegativo = dadoColetaDto.AceitaValorNegativo;
            IsObrigatorio = dadoColetaDto.IsObrigatorio;
            TipoDadoColeta = dadoColetaDto.TipoDadoColeta;
            TipoLimiteId = dadoColetaDto.TipoLimiteId;
            TipoPatamarId = dadoColetaDto.TipoPatamarId;
            GrandezaId = dadoColetaDto.GrandezaId;
            GrandezaNome = dadoColetaDto.GrandezaNome;
            GrandezaOrdemExibicao = dadoColetaDto.GrandezaOrdemExibicao;
            OrigemColetaNome = dadoColetaDto.OrigemColetaNome;
            OrigemColetaId = dadoColetaDto.OrigemColetaId;
            IsColetaPorEstagio = dadoColetaDto.IsColetaPorEstagio;
            TipoDadoGrandeza = dadoColetaDto.TipoDadoGrandeza;
            IsRecuperaValor = dadoColetaDto.IsRecuperaValor;
            IsDestacaDiferenca = dadoColetaDto.IsDestacaDiferenca;
            QuantidadeCasasInteira = dadoColetaDto.QuantidadeCasasInteira;
            QuantidadeCasasDecimais = dadoColetaDto.QuantidadeCasasDecimais;
        }

        public int Id { get; set; }
        public string Valor { get; set; }
        public string Estagio { get; set; }
        public string PeriodoSemana { get; set; }
        public bool IsColetaPorEstagio { get; set; }
        public bool IsRecuperaValor { get; set; }
        public bool IsDestacaDiferenca { get; set; }
        public string ValorEstudoAnterior { get; set; }
        public bool AceitaValorNegativo { get; set; }
        public bool IsObrigatorio { get; set; }
        public TipoDadoGrandezaEnum TipoDadoGrandeza { get; set; }
        public int QuantidadeCasasInteira { get; set; }
        public int QuantidadeCasasDecimais { get; set; }
        public bool? DestacaModificacao { get; set; }
        public short GrandezaOrdemExibicao { get; set; }

        public string OrigemColetaNome { get; set; }
    }
}
