using System.ComponentModel.DataAnnotations;

namespace ONS.WEBPMO.Application.Models.ColetaInsumo
{
    public class AlteracaoDadoColetaManutencaoModel : DadoColetaManutencaoModel
    {
        public int IdColetaInsumo { get; set; }

        public int IdDadoColeta { get; set; }

        [Display(Name = @"Usina")]
        public string NomeUsina { get; set; }

        [Display(Name = @"Unidade Geradora")]
        public string NomeUnidade { get; set; }
    }
}