namespace ONS.WEBPMO.Domain.Entities.PMO
{
    public class InsumoNaoEstruturado : Insumo
    {
        public bool IsUtilizadoDECOMP { get; set; }
        public bool IsUtilizadoConvergencia { get; set; }
        public bool IsUtilizadoPublicacao { get; set; }
        public bool IsUtilizadoProcessamento { get; set; }
    }
}
