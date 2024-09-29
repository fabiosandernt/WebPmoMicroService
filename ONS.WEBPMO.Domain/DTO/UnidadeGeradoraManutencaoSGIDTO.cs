
namespace ONS.WEBPMO.Domain.DTO
{
    public class UnidadeGeradoraManutencaoSGIDTO
    {
        public string NomeUnidadeGeradora { get; set; }
        public string IdUnidadeGeradora { get; set; }
        public int IdColetaInsumo { get; set; }
        public byte[] Versao { get; set; }
        public int IdGabarito { get; set; }
    }
}
