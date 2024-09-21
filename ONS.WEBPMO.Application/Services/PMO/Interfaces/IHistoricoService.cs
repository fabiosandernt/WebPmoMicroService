namespace ONS.WEBPMO.Application.Services.PMO.Interfaces
{
    [ServiceContract]
    public interface IHistoricoService : IService
    {
        [OperationContract]
        [TransactionRequired]
        void CriarSalvarHistoricoColetaInsumo(ColetaInsumo coletaInsumo);

        [OperationContract]
        [TransactionRequired]
        void CriarSalvarHistoricoSemanaOperativa(SemanaOperativa semanaOperativa);

        void ExcluirHistoricoColetaInsumo(int idColetaInsumo);

        void ExcluirHistoricoColetaInsumoViaSemanaOperativa(int idSemanaOperativa);

        void ExcluirHistoricoSemanaOperativa(int idSemanaOperativa);

        void ExcluirHistoricosSemanaOperativa(ISet<SemanaOperativa> idsSemanaOperativa);
    }
}
