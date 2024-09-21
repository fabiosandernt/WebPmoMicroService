namespace ONS.WEBPMO.Application.Services.PMO.Interfaces
{
    [ServiceContract]
    public interface IDadoColetaManutencaoService : IService
    {
        [OperationContract]
        [UseNetDataContractSerializer("ColetaInsumo")]
        DadoColetaManutencao ObterPorChave(int chave);

        [OperationContract]
        [UseNetDataContractSerializer]
        PagedResult<DadoColetaManutencaoDTO> ConsultarDadoColetaManutencaoPorColetaInsumo(
            DadoColetaInsumoFilter filter);


        [OperationContract]
        [UseNetDataContractSerializer("ColetaInsumo")]
        DadoColetaManutencao ObterPorColetaInsumoId(int idColetaInsumo);

        void IncluirDadoColeta(DadoColetaManutencao dadoColeta);
        void Excluir(DadoColetaManutencao dadoColeta);
        void AlterarDadoColeta(DadoColetaManutencao dadoColeta);
        void IncluirDadoColetaSeNaoExiste(IList<DadoColetaManutencao> dadoColetaList);
    }
}
