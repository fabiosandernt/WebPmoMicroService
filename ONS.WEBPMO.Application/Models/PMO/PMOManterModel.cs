using ONS.WEBPMO.Domain.Entities.Resources;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace ONS.WEBPMO.Application.Models.PMO
{
    public class PMOManterModel
    {
        public PMOManterModel()
        {
            SemanasOperativas = new List<SemanaOperativaModel>();
        }

        public int Id { get; set; }
        public IList<SemanaOperativaModel> SemanasOperativas { get; set; }
        public int MesReferencia { get; set; }
        public int AnoReferencia { get; set; }

        [Display(Name = "Meses a frente para Estudo")]
        [Range(0, 11, ErrorMessageResourceType = typeof(SGIPMOMessages), ErrorMessageResourceName = "MS009")]
        public int? QuantidadeMesesAdiante { get; set; }

        public byte[] Versao { get; set; }
        public string VersaoPmoString { get; set; }
        
        public string NomeMesReferencia
        {
            get
            {
                string mes = string.Empty;
                if (MesReferencia > 0)
                {
                    var cultura = CultureInfo.CurrentCulture;
                    mes = cultura.TextInfo.ToTitleCase(cultura.DateTimeFormat.GetMonthName(MesReferencia));
                }
                return mes;
            }
        }
    }
}