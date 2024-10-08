using ONS.WEBPMO.Domain.DTO;
using ONS.WEBPMO.Domain.Entities.Filters;

namespace ONS.WEBPMO.Domain.Presentations
{
    public interface IGabaritoPresentation  
    {
        
        //[UseNetDataContractSerializer("SemanasOperativas", "Agentes", "Insumos")]
        DadosFiltroPesquisaGabaritoDTO ObterDadosFiltroPesquisaGabarito(int? idInsumo, int? idAgente,
            int? idSemanaOperativa);

        
        
        DadosConfiguracaoGabaritoDTO ObterDadosConfiguracaoGabarito(GabaritoDadosFilter filter);

        
        
        DadosConfiguracaoGabaritoUnidadeGeradoraDTO ObterDadosConfiguracaoGabaritoUnidadeGeradora(
            GabaritoDadosFilter filter);

        
        
        DadosManutencaoGabaritoDTO ObterDadosManutencaoGabarito(GabaritoOrigemColetaFilter filtro);

        
        
        DadosConfiguracaoGabaritoDTO ObterDadosConfiguracaoGabaritoUnidadeGeradoraPorUsina(string idUsina);


    }
}
