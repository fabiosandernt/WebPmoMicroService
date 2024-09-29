namespace ONS.WEBPMO.Domain.DTO
{
    public class DadosInclusaoInsumoEstruturadoDTO
    {
        public DadosInclusaoInsumoEstruturadoDTO()
        {
            Grandezas = new List<ManutencaoGrandezaDTO>();
            Ativo = true;
        }

        public string VersaoStringInsumo { get; set; }
        public int CategoriaId { get; set; }
        public int TipoColetaId { get; set; }
        public int Id { get; set; }
        public string Nome { get; set; }
        public short? OrdemExibicao { get; set; }
        public bool IsPreAprovado { get; set; }

        public IList<ManutencaoGrandezaDTO> Grandezas { get; set; }

        public string SiglaInsumo { get; set; }
        public bool ExportarInsumo { get; set; }
        public bool Ativo { get; set; }
    }
}
