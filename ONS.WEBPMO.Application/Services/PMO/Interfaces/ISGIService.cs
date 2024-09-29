using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Application.Services.PMO.Interfaces
{
    
    public interface ISGIService 
    {
        
        
        IList<DadoColetaManutencao> ObterManutencoesPorChaves(string[] chavesUnidadesGeradoras,
            DateTime dataInicio, DateTime dataFim);
    }
}
