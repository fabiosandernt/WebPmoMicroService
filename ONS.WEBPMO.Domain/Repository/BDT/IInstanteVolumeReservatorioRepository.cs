using ONS.WEBPMO.Domain.Entities.BDT;
using ONS.WEBPMO.Domain.Repository.Base;

namespace ONS.WEBPMO.Domain.Repository.BDT
{
    public interface IInstanteVolumeReservatorioRepository : IRepository<InstanteVolumeReservatorio>
    {
        IList<InstanteVolumeReservatorio> Consultar(string usinaId, DateTime dataInicio, DateTime dataFim);
    }
}
