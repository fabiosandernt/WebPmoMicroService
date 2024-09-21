namespace ONS.WEBPMO.Application.DTO
{
    using System;
    using System.Collections.Generic;

    public class GabaritoConfiguracaoBaseDTO<TOrigemColetaConfig>
    {
        public GabaritoConfiguracaoBaseDTO()
        {
            ConfiguracaoDTOList = new List<TOrigemColetaConfig>();
        }

        public string NomeRevisao { get; set; }

        public IEnumerable<TOrigemColetaConfig> ConfiguracaoDTOList { get; set;}
    }
}
