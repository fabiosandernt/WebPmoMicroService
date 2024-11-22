namespace ONS.WEBPMO.Application.DTO
{

    public class GabaritoConfiguracaoBaseDTO<TOrigemColetaConfig>
    {
        public GabaritoConfiguracaoBaseDTO()
        {
            ConfiguracaoDTOList = new List<TOrigemColetaConfig>();
        }

        public string NomeRevisao { get; set; }

        public IEnumerable<TOrigemColetaConfig> ConfiguracaoDTOList { get; set; }
    }
}
