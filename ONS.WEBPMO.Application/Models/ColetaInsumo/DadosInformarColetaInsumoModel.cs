﻿using ONS.WEBPMO.Application.Models.ColetaInsumo.ColetaInsumo;

namespace ONS.WEBPMO.Application.Models.ColetaInsumo
{
    public class DadosInformarColetaInsumoModel
    {
        public object Header { get; set; }
        public string[,] DadosTela { get; set; }
        public ValorDadoColetaModel[,] Dados { get; set; }
        public ICollection<DadoColetaModel> DadosColetaInsumoPaginado { get; set; }
        public int TotalPaginas { get; set; }
    }
}