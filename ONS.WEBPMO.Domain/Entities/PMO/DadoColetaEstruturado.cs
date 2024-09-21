
namespace ONS.WEBPMO.Domain.Entities.PMO
{
    public class DadoColetaEstruturado : DadoColeta, IComparable<DadoColetaEstruturado>
    {
        public string Valor { get; set; }
        public string Estagio { get; set; }
        public virtual TipoPatamar TipoPatamar { get; set; }
        public virtual TipoLimite TipoLimite { get; set; }

        public bool? DestacaModificacao { get; set; }
        public int? TipoLimiteId { get; set; }
        public int? TipoPatamarId { get; set; }

        public int CompareTo(DadoColetaEstruturado obj)
        {
            if (string.IsNullOrWhiteSpace(obj.Estagio))
            {
                return -1;
            }
            string letraObj = obj.Estagio.Substring(0, 1);
            int numeroObj = int.Parse(obj.Estagio.Substring(1));

            string letra = Estagio.Substring(0, 1);
            int numero = int.Parse(Estagio.Substring(1));

            if (letra.Equals(letraObj))
            {
                return numero - numeroObj;
            }
            if (letra.Equals("S"))
            {
                return -1;
            }
            return 1;
        }
    }
}
