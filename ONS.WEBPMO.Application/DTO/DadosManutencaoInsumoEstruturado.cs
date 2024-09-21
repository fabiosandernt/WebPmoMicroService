using ONS.WEBPMO.Domain.Enumerations;

namespace ONS.WEBPMO.Application.DTO
{
    public class DadosManutencaoInsumoEstruturado
    {
        public DadosManutencaoInsumoEstruturado()
        {
            Categorias = new List<ChaveDescricaoDTO<int>>();
            TiposColeta = new List<ChaveDescricaoDTO<int>>();
            TipoInsumo = TipoInsumoEnum.Estruturado;
            Ativo = true;
        }

        public string VersaoStringInsumo { get; set; }
        public int CategoriaId { get; set; }
        public int TipoColetaId { get; set; }
        public int Id { get; set; }
        public string Nome { get; set; }
        public short? OrdemExibicao { get; set; }
        public bool? IsPreAprovado { get; set; }
        public string Reservado { get; set; }
        public bool IsReservado { get; set; }
        public bool PermiteAlteracao { get; set; }

        public string TipoInsumoNome
        {
            get { return TipoInsumo.ToDescription(); }
        }

        public TipoInsumoEnum TipoInsumo { get; set; }
        public IList<ChaveDescricaoDTO<int>> Categorias { get; set; }
        public IList<ChaveDescricaoDTO<int>> TiposColeta { get; set; }

        public string SiglaInsumo { get; set; }
        public bool? ExportarInsumo { get; set; }
        public bool Ativo { get; set; }
    }
}
