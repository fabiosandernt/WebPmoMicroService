using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ONS.WEBPMO.Application.Models.ColetaInsumo.ColetaInsumo
{
    public class DadoColetaModel : DadoColetaModelBase
    {
        public DadoColetaModel()
        {
            ValoresDadoColeta = new List<ValorDadoColetaModel>();
        }

        public DadoColetaModel(DadoColetaModel dadoColetaDto)
        {
            ValoresDadoColeta = new List<ValorDadoColetaModel>();
            TipoDadoColeta = dadoColetaDto.TipoDadoColeta;
            TipoLimiteId = dadoColetaDto.TipoLimiteId;
            TipoLimiteDescricao = dadoColetaDto.TipoLimiteDescricao;
            TipoPatamarId = dadoColetaDto.TipoPatamarId;
            TipoPatamarDescricao = dadoColetaDto.TipoPatamarDescricao;
            GrandezaId = dadoColetaDto.GrandezaId;
            GrandezaNome = dadoColetaDto.GrandezaNome;
            OrigemColetaId = dadoColetaDto.OrigemColetaId;
            OrigemColetaNome = dadoColetaDto.OrigemColetaNome;
            QuantidadeCasasInteira = dadoColetaDto.QuantidadeCasasInteira;
            QuantidadeCasasDecimais = dadoColetaDto.QuantidadeCasasDecimais;
        }
        public bool IsColetaPorPatamar { get; set; }
        public bool IsColetaPorLimite { get; set; }
        public bool IsRecuperaValor { get; set; }
        public bool IsDestacaDiferenca { get; set; }
        public bool IsColetaPorEstagio { get; set; }
        public IList<ValorDadoColetaModel> ValoresDadoColeta { get; set; }
        public int RowspanUsina { get; set; }
        public int RowspanGrandeza { get; set; }
        public int QuantidadeCasasInteira { get; set; }
        public int QuantidadeCasasDecimais { get; set; }
    }
}