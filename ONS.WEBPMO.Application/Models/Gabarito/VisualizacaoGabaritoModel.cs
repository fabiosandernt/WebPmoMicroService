using ONS.WEBPMO.Domain.Enumerations;
using System.Web.Mvc;

namespace ONS.WEBPMO.Application.Models.Gabarito
{
    public class VisualizarGabaritoModel
    {
        public string NomeGabarito { get; set; }
        public string NomeAgente { get; set; }
        public string NomePerfil { get; set; }
        public string NomeOrigemColeta { get; set; }
        public string CodigoPerfilONS { get; set; }

        public TipoOrigemColetaEnum? TipoOrigemColeta { get; set; }

        public IList<SelectListItem> OrigensColeta { get; set; }
        public IList<SelectListItem> OrigensColetaGabarito { get; set; }

        public IList<SelectListItem> Insumos { get; set; }
        public IList<SelectListItem> InsumosGabarito { get; set; }

        public string NomeTipoOrigemColeta
        {
            get
            {
                string nomeTipoOrigemColeta;

                switch (TipoOrigemColeta)
                {
                    case TipoOrigemColetaEnum.UnidadeGeradora:
                        nomeTipoOrigemColeta = "Unidade Geradora";
                        break;
                    case TipoOrigemColetaEnum.Reservatorio:
                        nomeTipoOrigemColeta = "Reservatório";
                        break;
                    case TipoOrigemColetaEnum.GeracaoComplementar:
                        nomeTipoOrigemColeta = "Geração Complementar";
                        break;
                    case null:
                        nomeTipoOrigemColeta = "Não estruturado";
                        break;
                    default:
                        nomeTipoOrigemColeta = TipoOrigemColeta.ToString();
                        break;
                }

                return nomeTipoOrigemColeta;
            }
        }
    }
}