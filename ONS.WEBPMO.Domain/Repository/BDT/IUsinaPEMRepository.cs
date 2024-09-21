
using ONS.WEBPMO.Domain.Entities.BDT;
using ONS.WEBPMO.Domain.Repository.Base;

namespace ONS.WEBPMO.Domain.Repository.BDT
{
    public interface IUsinaPEMRepository : IRepository<UsinaPEM>
    {
        /// <summary>
        /// Consulta usinas do PEM
        /// </summary>
        /// <returns>Lista de Usinas do PEM</returns>
        IList<UsinaPEM> ConsultarDadosUsinasVisaoPEM();
    }

}
