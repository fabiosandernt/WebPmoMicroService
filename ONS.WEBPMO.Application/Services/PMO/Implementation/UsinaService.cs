using ONS.Common.Services.Impl;
using ONS.WEBPMO.Application.Services.PMO.Interfaces.OrigemColeta;
using ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    public class UsinaService : Service, IUsinaService
    {
        public Usina ObterUsinaPorChave(int chave)
        {
            throw new NotImplementedException();
        }

        public IList<Usina> ConsultarUsinasPorChaves(params int[] chaves)
        {
            throw new NotImplementedException();
        }

        public IList<Usina> ConsultarUsinasPorNome(string nome)
        {
            throw new NotImplementedException();
        }
    }
}
