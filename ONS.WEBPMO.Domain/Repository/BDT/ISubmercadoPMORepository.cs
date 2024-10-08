using ONS.WEBPMO.Domain.Entities.BDT;
using ONS.WEBPMO.Domain.Repository.Base;

namespace ONS.WEBPMO.Domain.Repository.BDT
{
    public interface ISubmercadoPMORepository : IRepository<SubmercadoPMO>
    {
        /// <summary>
        /// Consulta todos os Submercados na BDT
        /// </summary>
        /// <returns></returns>
        IList<SubmercadoPMO> ConsultarTodos();
    }
}
