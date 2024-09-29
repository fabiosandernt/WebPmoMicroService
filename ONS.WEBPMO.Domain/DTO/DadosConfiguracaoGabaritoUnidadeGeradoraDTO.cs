
namespace ONS.WEBPMO.Domain.DTO
{
    public class DadosConfiguracaoGabaritoUnidadeGeradoraDTO : DadosConfiguracaoGabaritoDTO
    {
        public DadosConfiguracaoGabaritoUnidadeGeradoraDTO()
        {
            UnidadesGeradoras = new List<ChaveDescricaoDTO<string>>();
        }

        public IList<ChaveDescricaoDTO<string>> UnidadesGeradoras { get; set; }
    }
}
