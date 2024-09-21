using ONS.WEBPMO.Application.Services.PMO.Interfaces;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    public class GrandezaService : Service, IGrandezaService
    {
        private IGrandezaRepository GrandezaRepository { get; set; }

        public GrandezaService(IGrandezaRepository GrandezaRepository)
        {
            this.GrandezaRepository = GrandezaRepository;
        }

        public Grandeza ObterPorChave(int chave)
        {
            return GrandezaRepository.FindByKey(chave);
        }
    }
}
