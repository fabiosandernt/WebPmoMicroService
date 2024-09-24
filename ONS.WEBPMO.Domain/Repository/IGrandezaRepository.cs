
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository.Base;

namespace ONS.WEBPMO.Domain.Repository
{
    public interface IGrandezaRepository : IRepository<Grandeza>
    {
        bool ExisteGrandezaPorEstagio(int idInsumo);
        bool ExistePreAprovadoComAlteracao(int idInsumo);
        bool ExisteGrandezaPorPatamar(int idInsumo);
        bool ExisteGrandezaPorLimite(int idInsumo);
        bool ExisteDadosColetaNaGrandeza(int idGrandeza);

        IList<Grandeza> ConsultarPorFiltro(GrandezaFilter filter);

        Grandeza ConsultarPorNome(string nomeGrandeza);

        IList<Grandeza> ConsultarPorInsumo(int idInsumo);
    }
}
