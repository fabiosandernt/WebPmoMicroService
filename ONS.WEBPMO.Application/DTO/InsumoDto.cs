namespace ONS.WEBPMO.Application.DTO
{
    public class InsumoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public short OrdemExibicao { get; set; }
        public bool PreAprovado { get; set; }
        public bool Reservado { get; set; }
        public string TipoInsumo { get; set; }
        public DateTime? DataUltimaAtualizacao { get; set; }
        public string VersaoStringInsumo { get; set; }
        public string SiglaInsumo { get; set; }
        public bool ExportarInsumo { get; set; }
        public bool Ativo { get; set; }
    }
}
