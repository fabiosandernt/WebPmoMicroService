using ONS.Common.Services.Impl;
using ONS.WEBPMO.Application.Services.PMO.Interfaces.OrigemColeta;
using ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    public class ReservatorioService : Service, IReservatorioService
    {
        public IList<Reservatorio> ConsultarReservatoriosPorChaves(params int[] chaves)
        {
            throw new NotImplementedException();
        }

        public IList<Reservatorio> ConsultarReservatoriosPorNome(string nome)
        {
            throw new NotImplementedException();
        }

        public Reservatorio ObterReservatorioPorChave(int chave)
        {
            throw new NotImplementedException();
        }
    }
}
