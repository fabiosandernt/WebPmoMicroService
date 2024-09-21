using ONS.Common.Services.Impl;
using ONS.SGIPMO.Domain.Entities;
using ONS.WEBPMO.Application.Services.PMO.Interfaces.OrigemColeta;
using System;
using System.Collections.Generic;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    public class ReservatorioService : Service, IReservatorioService
    {
        public Reservatorio ObterReservatorioPorChave(int chave)
        {
            throw new NotImplementedException();
        }

        public IList<Reservatorio> ConsultarReservatoriosPorChaves(params int[] chaves)
        {
            throw new NotImplementedException();
        }

        public IList<Reservatorio> ConsultarReservatoriosPorNome(string nome)
        {
            throw new NotImplementedException();
        }
    }
}
