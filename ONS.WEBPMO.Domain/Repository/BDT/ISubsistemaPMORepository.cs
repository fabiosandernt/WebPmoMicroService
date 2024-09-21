
using ONS.WEBPMO.Domain.Entities.BDT;
using ONS.WEBPMO.Domain.Repository.Base;

namespace ONS.WEBPMO.Domain.Repository.BDT
{
    public interface ISubsistemaPMORepository : IRepository<SubsistemaPMO>
    {
        /// <summary>
        /// Consulta todos os subsistemas na BDT
        /// </summary>
        /// <returns></returns>
        IList<SubsistemaPMO> ConsultarTodos();
        IList<SubsistemaPMO> ConsultarAtivos();
        IList<SubsistemaPMO> ConsultarOutros();

    }
}
