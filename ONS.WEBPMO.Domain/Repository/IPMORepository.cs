using ONS.WEBPMO.Domain.Repository.Base;

namespace ONS.WEBPMO.Domain.Repository
{
    public interface IPMORepository : IRepository<PMO>
    {
        PMO ObterPorFiltro(PMOFilter filtro);
        PMO ObterPorFiltroExterno(PMOFilter filtro);
        int ObterQuantidadeSemanasPMO(int idSemanaOperativa);
    }
}
