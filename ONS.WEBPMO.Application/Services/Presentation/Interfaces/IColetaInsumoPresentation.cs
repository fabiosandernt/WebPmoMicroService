namespace ONS.WEBPMO.Domain.Presentations
{
    [ServiceContract]
    public interface IColetaInsumoPresentation : IService
    {
        [OperationContract]
        [UseNetDataContractSerializer("SemanasOperativas", "Agentes", "Insumos", "SituacoesColeta")]
        DadosPesquisaColetaInsumoDTO ObterDadosPesquisaColetaInsumo(
            int? idSemanaOperativa = null, bool isMonitorar = false, bool? ordernarListagens = true);

        [OperationContract]
        [UseNetDataContractSerializer]
        DadosInclusaoDadoColetaManutencaoDTO ObterDadosInclusaoDadoColetaManutencao(int idColetaInsumo);

        [OperationContract]
        [UseNetDataContractSerializer]
        DadosAlteracaoDadoColetaManutencaoDTO ObterDadosAlteracaoDadoColetaManutencao(int idDadoColeta);

        [OperationContract]
        [UseNetDataContractSerializer("ColetaInsumo", "UnidadeGeradora", "UnidadeGeradora.Usina")]
        IList<DadoColetaManutencao> ConsultarManutencaoSGI(int idColetaInsumo);

        [OperationContract]
        [UseNetDataContractSerializer]
        DadosPesquisaGeracaoBlocosDTO ObterDadosPesquisaGeracaoBloco(int idSemanaOperativa);

        [OperationContract]
        [UseNetDataContractSerializer]
        void ImportarCronogramaManutencaoHidraulicaTermica(int idSemanaOperativa, IList<int> idsInsumo);
    }
}
