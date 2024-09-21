using System;
using System.Collections.Generic;
using ONS.Common.Util.Control;

namespace ONS.WEBPMO.Application.DTO
{
    public class ArquivosSemanaOperativaDTO : ICloneablePath
    {

        public ArquivosSemanaOperativaDTO()
        {
            ArquivosInsumos = new List<ArquivoDadoNaoEstruturadoConsultaInsumoDTO>();
            ArquivosEnviados = new List<ArquivoDadoNaoEstruturadoDTO>();
        }

        public SituacaoSemanaOperativa SituacaoSemanaOperativa { get; set; }

        public IList<ArquivoDadoNaoEstruturadoConsultaInsumoDTO> ArquivosInsumos { get; set; }

        public IList<ArquivoDadoNaoEstruturadoDTO> ArquivosEnviados { get; set; }

        public Type GetRealType()
        {
            return this.GetType();
        }

    }
}
