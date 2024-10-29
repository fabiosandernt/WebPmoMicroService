using ONS.WEBPMO.Application.Models.PMO;

namespace ONS.WEBPMO.Application.DTO
{
    public class PMODTO
    {
        public int Id { get; set; } 
        public int AnoReferencia { get; set; }
        public int MesReferencia { get; set; }
        public int? QuantidadeMesesAdiante { get; set; }
        public string Versao { get; set; } 
        public ICollection<SemanaOperativaModel> SemanasOperativas { get; set; } = new HashSet<SemanaOperativaModel>();

        
    }
}
