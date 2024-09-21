
using System.Collections.Generic;

namespace ONS.WEBPMO.Application.Models.Insumo
{
    public class ListagemGrandezaModel
    {
        public ListagemGrandezaModel()
        {
            Grandezas = new List<ManutencaoGrandezaModel>();
        }

        public int IdInsumo { get; set; }
        public IList<ManutencaoGrandezaModel> Grandezas { get; set; }
    }
}