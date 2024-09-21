using ONS.Common.Services.Impl;
using ONS.SGIPMO.Domain.Entities;
using ONS.WEBPMO.Application.Services.PMO.Interfaces.OrigemColeta;
using System;
using System.Collections.Generic;

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
