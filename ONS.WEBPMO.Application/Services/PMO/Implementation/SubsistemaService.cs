using ONS.WEBPMO.Application.Services.PMO.Interfaces.OrigemColeta;
using ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    public class SubsistemaService : ISubsistemaService
    {
        public Subsistema ObterSubsistemaPorChave(int chave)
        {
            throw new NotImplementedException();
        }

        public IList<Subsistema> ConsultarSubsistemasPorChaves(params int[] chaves)
        {
            throw new NotImplementedException();
        }

        public IList<Subsistema> ConsultarSubsistemasPorNome(string nome)
        {
            throw new NotImplementedException();
        }
    }
}
