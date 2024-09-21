using ONS.WEBPMO.Application.Services.PMO.Interfaces.OrigemColeta;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    public class UnidadeGeradoraService : Service, IUnidadeGeradoraService
    {
        public UnidadeGeradora ObterUnidadeGeradoraPorChave(int chave)
        {
            throw new NotImplementedException();
        }

        public IList<UnidadeGeradora> ConsultarUnidadesGeradorasPorChaves(params int[] chaves)
        {
            throw new NotImplementedException();
        }

        public IList<UnidadeGeradora> ConsultarUnidadesGeradorasPorNome(string nome)
        {
            throw new NotImplementedException();
        }
    }
}
