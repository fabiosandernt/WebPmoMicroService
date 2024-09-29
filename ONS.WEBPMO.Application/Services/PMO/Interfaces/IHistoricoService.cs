using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Application.Services.PMO.Interfaces
{
    //[ServiceContract]
    public interface IHistoricoService 
    {
        
        
        void CriarSalvarHistoricoColetaInsumo(ColetaInsumo coletaInsumo);

        
        
        void CriarSalvarHistoricoSemanaOperativa(SemanaOperativa semanaOperativa);

        void ExcluirHistoricoColetaInsumo(int idColetaInsumo);

        void ExcluirHistoricoColetaInsumoViaSemanaOperativa(int idSemanaOperativa);

        void ExcluirHistoricoSemanaOperativa(int idSemanaOperativa);

        void ExcluirHistoricosSemanaOperativa(ISet<SemanaOperativa> idsSemanaOperativa);
    }
}
