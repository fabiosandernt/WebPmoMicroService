using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository.Base;

namespace ONS.WEBPMO.Domain.Repository.PMO
{
    public interface IDadoColetaNaoEstruturadoRepository : IRepository<DadoColetaNaoEstruturado>
    {
        DadoColetaNaoEstruturado ObterDadoColetaNaoEstruturado(DadoColetaInsumoNaoEstruturadoFilter filtros);
        void DeletarPorIdGabarito(IList<int> idsGabarito);
        IList<DadoColetaNaoEstruturado> ObterDadosColetaNaoEstruturado(ArquivosSemanaOperativaFilter filtro);
    }
}
