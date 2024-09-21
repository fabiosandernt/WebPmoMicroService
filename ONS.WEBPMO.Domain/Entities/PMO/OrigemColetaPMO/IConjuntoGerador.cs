

namespace ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO
{
    public interface IConjuntoGerador
    {
        string Id { get; }
        int CodigoDPP { get; }
        string IdSubsistema { get; set; }
        Subsistema Subsistema { get; }
        string NomeCurto { get; }
    }
}
