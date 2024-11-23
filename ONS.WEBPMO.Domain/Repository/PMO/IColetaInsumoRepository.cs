using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository.Base;

namespace ONS.WEBPMO.Domain.Repository.PMO
{
    public interface IColetaInsumoRepository : IRepository<ColetaInsumo>
    {
        bool Any(int idAgente, int idInsumo, int idSemanaOperativa, string codigoPerfilOns);
        bool AnyNaoAprovado(int idSemanaOperativa);

        IList<ColetaInsumo> ConsultarColetasInsumoOrfaos(int idAgente, int idSemanaOperativa, string codigoPerfilONS);
        IList<ColetaInsumo> FindByKeys(IList<int> idsColetaInsumo);
        ColetaInsumo GetByKey(int idsColetaInsumo);

        ICollection<ColetaInsumo> ConsultarParaInformarDados(PesquisaMonitorarColetaInsumoFilter filter);

        ColetaInsumo ObterColetaInsumoAnterior(ColetaInsumo coletaInsumoAtual);

        ColetaInsumo ObterColetaInsumoSemanaOperativaAnterior(ColetaInsumo coletaInsumoAtual);

        void DeletarPorIdSemanaOperativa(int idSemanaOperativa);

        IList<ColetaInsumo> ConsultarColetaParticipaBloco(int idSemanaOperativa);

        IList<ColetaInsumo> ObterColetaInsumoPorSemanaOperativaInsumos(int idSemanaOperativa, IList<int> idInsumos);
    }
}
