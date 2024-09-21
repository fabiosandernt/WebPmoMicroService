using System.Web.Mvc;


namespace ONS.WEBPMO.Application.Models.PMO
{
    public class EscolhaGabaritoModel
    {
        public EscolhaGabaritoModel()
        {
            Estudos = new List<SelectListItem>();
            EstudosParaDesconsiderar = new List<SelectListItem>();
        }

        public int IdSemanaOperativa { get; set; }
        public int? IdEstudo { get; set; }
        public IList<SelectListItem> Estudos { get; set; }
        public IList<SelectListItem> EstudosParaDesconsiderar { get; set; }
        public bool IsPadrao { get; set; }
        public byte[] Versao { get; set; }
        public byte[] VersaoPMO { get; set; }
        public byte[] VersaoSemanaOperativa { get; set; }
    }
}