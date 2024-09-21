using System;
using ONS.Common.Util.Control;

namespace ONS.WEBPMO.Application.DTO
{
    public class ArquivoDadoNaoEstruturadoConsultaInsumoDTO : ICloneablePath
    {
        public ArquivoDadoNaoEstruturadoDTO Arquivo { get; set; }

        public String DescricaoInsumo { get; set; }
        
        public int IdColetaInsumo { get; set; }
        
        public Type GetRealType()
        {
            return this.GetType();
        }
    }
}
