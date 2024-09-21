using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using ONS.Common.Util;
using ONS.SGIPMO.Domain.Entities;

namespace ONS.WEBPMO.Application.Models.Insumo
{
    public class InsumoConsultaModel
    {
        public bool IsPrimeiroCarregamentoGrid { get; set; }

        [StringLength(150)]
        public string Nome { get; set; }

        public string TipoInsumo { get; set; }

        public IList<SelectListItem> TiposInsumo
        {
            get
            {
                IList<SelectListItem> lista = new List<SelectListItem>
                {
                    new SelectListItem {Text = TipoInsumoEnum.Estruturado.ToDescription(), Value = TipoInsumoEnum.Estruturado.ToChar()},
                    new SelectListItem {Text = TipoInsumoEnum.NaoEstruturado.ToDescription(), Value = TipoInsumoEnum.NaoEstruturado.ToChar()}
                };
                return lista;
            }
        }

        public int? CategoriaId { get; set; }
        public IList<SelectListItem> Categorias { get; set; }

        public string SiglaInsumo { get; set; }

        public int? TipoColetaId { get; set; }
        public IList<SelectListItem> TiposColeta { get; set; }
    }
}