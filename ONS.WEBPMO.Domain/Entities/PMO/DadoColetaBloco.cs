using ONS.WEBPMO.Domain.Enumerations;

namespace ONS.WEBPMO.Domain.Entities.PMO
{
    public class DadoColetaBloco : IComparable<DadoColetaBloco>
    {
        public ColetaInsumo ColetaInsumo { get; set; }
        public Gabarito Gabarito { get; set; }
        public InsumoEstruturado Insumo { get; set; }
        public Grandeza Grandeza { get; set; }
        public TipoPatamarEnum? TipoPatamar { get; set; }
        public TipoLimiteEnum? TipoLimite { get; set; }
        public string Estagio { get; set; }

        public DadoColetaBloco() { }

        public DadoColetaBloco(DadoColetaBloco dadoColetaBloco)
        {
            ColetaInsumo = dadoColetaBloco.ColetaInsumo;
            Gabarito = dadoColetaBloco.Gabarito;
            Insumo = dadoColetaBloco.Insumo;
            Grandeza = dadoColetaBloco.Grandeza;
            TipoPatamar = dadoColetaBloco.TipoPatamar;
            TipoLimite = dadoColetaBloco.TipoLimite;
            Estagio = Estagio;
        }

        public int CompareTo(DadoColetaBloco obj)
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
