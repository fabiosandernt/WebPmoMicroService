

using ONS.WEBPMO.Domain.Entities.BDT;
using ONS.WEBPMO.Domain.Repository.Base;

namespace OONS.WEBPMO.Domain.Repository.BDT
{
    public interface IReservatorioEERepository : IRepository<ReservatorioEE>
    {
        /// <summary>
        /// Consulta dados de todos os REEs ativos
        /// </summary>
        /// <returns>Lista de todos os REEs ativos</returns>
        IList<ReservatorioEE> ConsultarReservatoriosEquivalentesDeEnergiaAtivos();
    }

}
