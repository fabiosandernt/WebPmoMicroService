using ONS.WEBPMO.Domain.Enumerations;

namespace ONS.WEBPMO.Application.DTO
{
    
    public class GabaritoConfiguracaoDTO
    {
        public GabaritoConfiguracaoDTO()
        {
            IdsInsumo = new List<int>();
            IdsOrigemColeta = new List<string>();
        }

        public GabaritoConfiguracaoDTO(GabaritoConfiguracaoDTO dto)
        {
            IdAgente = dto.IdAgente;
            IdSemanaOperativa = dto.IdSemanaOperativa;
            IsPadrao = dto.IsPadrao;
            TipoOrigemColeta = dto.TipoOrigemColeta;
            IdsInsumo = dto.IdsInsumo;
            IdsOrigemColeta = dto.IdsOrigemColeta;
            CodigoPerfilONS = dto.CodigoPerfilONS;
        }

        public string IdOrigemColetaPai { get; set; }
        public int IdAgente { get; set; }
        public int? IdSemanaOperativa { get; set; }
        public bool IsPadrao { get; set; }
        public string CodigoPerfilONS { get; set; }
        
        public TipoOrigemColetaEnum TipoOrigemColeta { get; set; }
        public TipoInsumoEnum TipoInsumo { get; set; }

        public IList<string> IdsOrigemColeta { get; set; }
        public IList<int> IdsInsumo { get; set; }
    }
}
