using ONS.Common.Services.Impl;
using ONS.SGIPMO.Domain.Entities;
using ONS.SGIPMO.Domain.Repositories;
using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
