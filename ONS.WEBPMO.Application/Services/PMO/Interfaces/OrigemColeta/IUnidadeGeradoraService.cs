using ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO;

namespace ONS.WEBPMO.Application.Services.PMO.Interfaces.OrigemColeta
{
    //[ServiceContract]
    public interface IUnidadeGeradoraService 
    {
        
        
        UnidadeGeradora ObterUnidadeGeradoraPorChave(int chave);

        
        
        IList<UnidadeGeradora> ConsultarUnidadesGeradorasPorChaves(params int[] chaves);

        
        
        IList<UnidadeGeradora> ConsultarUnidadesGeradorasPorNome(string nome);
    }
}
