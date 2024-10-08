using ONS.Common.Util.Pagination;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository;
using ONS.WEBPMO.Infrastructure.Context;
using ONS.WEBPMO.Infrastructure.DataBase;

namespace ONS.WEBPMO.Domain.Repositories.Impl
{
    public class ColetaInsumoRepository : Repository<ColetaInsumo>, IColetaInsumoRepository
    {
        public ColetaInsumoRepository(WEBPMODbContext context) : base(context)
        {
        }

        public bool Any(int idAgente, int idInsumo, int idSemanaOperativa, string codigoPerfilOns)
        {
            throw new NotImplementedException();
        }

        public bool AnyNaoAprovado(int idSemanaOperativa)
        {
            throw new NotImplementedException();
        }

        public IList<ColetaInsumo> ConsultarColetaParticipaBloco(int idSemanaOperativa)
        {
            throw new NotImplementedException();
        }

        public IList<ColetaInsumo> ConsultarColetasInsumoOrfaos(int idAgente, int idSemanaOperativa, string codigoPerfilONS)
        {
            throw new NotImplementedException();
        }

        public PagedResult<ColetaInsumo> ConsultarParaInformarDados(PesquisaMonitorarColetaInsumoFilter filter)
        {
            throw new NotImplementedException();
        }

        public void DeletarPorIdSemanaOperativa(int idSemanaOperativa)
        {
            throw new NotImplementedException();
        }

        public IList<ColetaInsumo> FindByKeys(IList<int> idsColetaInsumo)
        {
            throw new NotImplementedException();
        }

        public ColetaInsumo GetByKey(int idsColetaInsumo)
        {
            throw new NotImplementedException();
        }

        public ColetaInsumo ObterColetaInsumoAnterior(ColetaInsumo coletaInsumoAtual)
        {
            throw new NotImplementedException();
        }

        public IList<ColetaInsumo> ObterColetaInsumoPorSemanaOperativaInsumos(int idSemanaOperativa, IList<int> idInsumos)
        {
            throw new NotImplementedException();
        }

        public ColetaInsumo ObterColetaInsumoSemanaOperativaAnterior(ColetaInsumo coletaInsumoAtual)
        {
            throw new NotImplementedException();
        }
    }
}
