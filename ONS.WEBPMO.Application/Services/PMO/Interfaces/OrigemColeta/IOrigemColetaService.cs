namespace ONS.WEBPMO.Application.Services.PMO.Interfaces.OrigemColeta
{
    [ServiceContract]
    public interface IOrigemColetaService : IService
    {
        [OperationContract]
        [UseNetDataContractSerializer]
        PagedResult<OrigemColeta> ConsultarOrigemColetasGabaritoPaginado(GabaritoParticipantesFilter filter);

        T ObterOrigemColetaPorChaveOnline<T>(string id) where T : OrigemColeta;

        T ObterOrigemColetaPorChave<T>(string id) where T : OrigemColeta;

        [OperationContract]
        [UseNetDataContractSerializer]
        OrigemColeta ObterOuCriarOrigemColetaPorId(string idOrigemColeta, TipoOrigemColetaEnum tipoOrigemColeta);

        [OperationContract]
        [UseNetDataContractSerializer]
        IList<OrigemColeta> ConsultarOuCriarOrigemColetaPorIds(IList<string> idsOrigemColeta, TipoOrigemColetaEnum tipoOrigemColeta);

        [OperationContract]
        [UseNetDataContractSerializer]
        IList<OrigemColeta> ConsultarOrigemColetaPorTipoNomeOnline(TipoOrigemColetaEnum tipoOrigemColeta, string nome);

        [OperationContract]
        [UseNetDataContractSerializer]
        IList<UnidadeGeradora> ConsultarUnidadeGeradoraPorUsinaOnline(string idOrigemColeta);

        [OperationContract]
        [UseNetDataContractSerializer]
        IList<Usina> ConsultarUsinaParticipanteGabaritoPorColetaInsumo(int idColetaInsumo);

        [OperationContract]
        [UseNetDataContractSerializer]
        IList<UnidadeGeradora> ConsultarUnidadeGeradoParticipanteGabaritoPorColetaInsumoUsina(
            int idColetaInsumo, string idUsina);

        [OperationContract]
        [UseNetDataContractSerializer]
        IList<UnidadeGeradora> ConsultarUnidadeGeradoParticipanteGabaritoPorColetaInsumo(int idColetaInsumo);

        [OperationContract]
        [UseNetDataContractSerializer]
        IList<UnidadeGeradoraManutencaoSGIDTO> ConsultarUnidadeGeradoParticipanteGabaritoPorColetaInsumos(List<int> idColetaInsumos);

        [TransactionRequired]
        void SincronizarOrigensColetaComBDT();
    }
}
