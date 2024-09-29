namespace ONS.WEBPMO.Domain.DTO
{
    public class ArquivoDadoNaoEstruturadoConsultaInsumoDTO //: ICloneablePath
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
