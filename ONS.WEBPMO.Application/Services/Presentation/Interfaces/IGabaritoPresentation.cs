namespace ONS.WEBPMO.Domain.Presentations
{
    using Entities.Filters;

    [ServiceContract]
    public interface IGabaritoPresentation : IService
    {
        [OperationContract]
        [UseNetDataContractSerializer("SemanasOperativas", "Agentes", "Insumos")]
        DadosFiltroPesquisaGabaritoDTO ObterDadosFiltroPesquisaGabarito(int? idInsumo, int? idAgente,
            int? idSemanaOperativa);

        [OperationContract]
        [UseNetDataContractSerializer]
        DadosConfiguracaoGabaritoDTO ObterDadosConfiguracaoGabarito(GabaritoDadosFilter filter);

        [OperationContract]
        [UseNetDataContractSerializer]
        DadosConfiguracaoGabaritoUnidadeGeradoraDTO ObterDadosConfiguracaoGabaritoUnidadeGeradora(
            GabaritoDadosFilter filter);

        [OperationContract]
        [UseNetDataContractSerializer]
        DadosManutencaoGabaritoDTO ObterDadosManutencaoGabarito(GabaritoOrigemColetaFilter filtro);

        [OperationContract]
        [UseNetDataContractSerializer]
        DadosConfiguracaoGabaritoDTO ObterDadosConfiguracaoGabaritoUnidadeGeradoraPorUsina(string idUsina);


    }
}
