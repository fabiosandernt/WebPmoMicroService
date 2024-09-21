using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ONS.WEBPMO.Application.DTO
{
    public class DadoColetaDTO : DadoColetaDTOBase
    {
        public DadoColetaDTO()
        {
            ValoresDadoColeta = new List<ValorDadoColetaDTO>();
        }

        public DadoColetaDTO(DadoColetaDTO dadoColetaDto)
        {
            ValoresDadoColeta = new List<ValorDadoColetaDTO>();
            TipoDadoColeta = dadoColetaDto.TipoDadoColeta;
            TipoLimiteId = dadoColetaDto.TipoLimiteId;
            TipoLimiteDescricao = dadoColetaDto.TipoLimiteDescricao;
            TipoPatamarId = dadoColetaDto.TipoPatamarId;
            TipoPatamarDescricao = dadoColetaDto.TipoPatamarDescricao;
            GrandezaId = dadoColetaDto.GrandezaId;
            GrandezaNome = dadoColetaDto.GrandezaNome;
            GrandezaOrdemExibicao = dadoColetaDto.GrandezaOrdemExibicao;
            OrigemColetaId = dadoColetaDto.OrigemColetaId;
            OrigemColetaNome = dadoColetaDto.OrigemColetaNome;
            IsColetaPorPatamar = dadoColetaDto.IsColetaPorPatamar;
            IsColetaPorLimite = dadoColetaDto.IsColetaPorLimite;
            IsRecuperaValor = dadoColetaDto.IsRecuperaValor;
            IsDestacaDiferenca = dadoColetaDto.IsDestacaDiferenca;
            IsColetaPorEstagio = dadoColetaDto.IsColetaPorEstagio;
            TipoDadoGrandeza = dadoColetaDto.TipoDadoGrandeza;
            AceitaValorNegativo = dadoColetaDto.AceitaValorNegativo;
            IsObrigatorio = dadoColetaDto.IsObrigatorio;
            QuantidadeCasasInteira = dadoColetaDto.QuantidadeCasasInteira;
            QuantidadeCasasDecimais = dadoColetaDto.QuantidadeCasasDecimais;
        }

        public string TipoLimiteDescricao { get; set; }
        public string TipoPatamarDescricao { get; set; }
        public string OrigemColetaNome { get; set; }
        public bool IsColetaPorPatamar { get; set; }
        public bool IsColetaPorLimite { get; set; }
        public bool IsRecuperaValor { get; set; }
        public bool IsDestacaDiferenca { get; set; }
        public bool IsColetaPorEstagio { get; set; }
        public bool AceitaValorNegativo { get; set; }
        public bool IsObrigatorio { get; set; }
        public TipoDadoGrandezaEnum TipoDadoGrandeza { get; set; }
        public IList<ValorDadoColetaDTO> ValoresDadoColeta { get; set; }
        public int RowspanUsina { get; set; }
        public int RowspanGrandeza { get; set; }
        public int QuantidadeCasasInteira { get; set; }
        public int QuantidadeCasasDecimais { get; set; }
        public short GrandezaOrdemExibicao { get; set; }
    }
}
