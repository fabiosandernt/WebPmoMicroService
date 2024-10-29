using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    public class GrandezaService : IGrandezaService
    {
        private IGrandezaRepository GrandezaRepository { get; set; }

        public GrandezaService(IGrandezaRepository GrandezaRepository)
        {
            this.GrandezaRepository = GrandezaRepository;
        }

        public Grandeza ObterPorChave(int chave)
        {
            throw new NotImplementedException();
        }
    }
}
