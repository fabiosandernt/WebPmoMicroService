namespace ONS.WEBPMO.Domain.Entities.Usina.OrigemColetaUsina
{
    public class Reservatorio : OrigemColeta
    {
        public virtual Subsistema Subsistema { get; set; }

        public int CodigoDPP { get; set; }

        public String NomeLongo { get; set; }

        public String NomeCurto { get; set; }

        public String IdSubsistema { get; set; }

        public int? Cod_subsistemamodenerg { get; set; }

        public string Cod_reservatorioee { get; set; }

        public string Nom_curto_reservatorioee { get; set; }

        public string CodUsiPlanejamentoJusante { get; set; }

    }
}
