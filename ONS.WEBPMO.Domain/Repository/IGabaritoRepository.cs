using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository.Base;

namespace ONS.WEBPMO.Domain.Repository
{
    public interface IGabaritoRepository : IRepository<Gabarito>
    {

        IList<Gabarito> ConsultarPorGabaritoFilter(GabaritoConfiguracaoFilter filter);

        ICollection<Gabarito> ConsultarPorGabaritoFilterPaginado(GabaritoConfiguracaoFilter filter);

        Gabarito ObterPorColetaInumoOrigemColeta(int idColetaInsumo, string idOrigemColeta);

        IEnumerable<Gabarito> ConsultarParaRemover(IList<int> ids);

        void DeletarPorIdSemanaOperativa(int idSemanaOperativa);

        IList<Gabarito> ConsultarGabaritoParticipaBloco(int idSemanaOperativa);



    }
}
