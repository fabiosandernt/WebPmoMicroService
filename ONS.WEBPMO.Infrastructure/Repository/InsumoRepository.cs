using ONS.Common.Util.Pagination;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Enumerations;
using ONS.WEBPMO.Domain.Repository;
using ONS.WEBPMO.Infrastructure.Context;
using ONS.WEBPMO.Infrastructure.DataBase;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Repositories
{ 

    public class InsumoRepository : Repository<Insumo>, IInsumoRepository
    {
        public InsumoRepository(WEBPMODbContext context) : base(context)
        {
        }

        public IList<Insumo> ConsultaInsumoPorIds(params int[] ids)
        {
            throw new NotImplementedException();
        }

        public IList<InsumoEstruturado> ConsultarInsumoEstruturadoComGrandezaAtiva(TipoColetaEnum tipoColeta, CategoriaInsumoEnum? categoria = null)
        {
            throw new NotImplementedException();
        }

        public IList<InsumoNaoEstruturado> ConsultarInsumoNaoEstruturado()
        {
            throw new NotImplementedException();
        }

        public IList<Insumo> ConsultarInsumosNaoEstruturadoEEstruturadoComGrandezaAtiva()
        {
            throw new NotImplementedException();
        }

        public IList<Insumo> ConsultarInsumosPorSemanaOperativaAgentes(int idSemanaOperativa, params int[] idsAgente)
        {
            throw new NotImplementedException();
        }

        public IList<Insumo> ConsultarPorInsulmoFiltro(InsumoFiltro filtro)
        {
            throw new NotImplementedException();
        }

        public PagedResult<Insumo> ConsultarPorInsumoFiltroPaginado(InsumoFiltro filtro)
        {
            throw new NotImplementedException();
        }

        public Insumo ConsultarPorNome(string nomeInsumo)
        {
            throw new NotImplementedException();
        }

        public IList<Insumo> ConsultarPorNomeLike(string nomeInsumo)
        {
            throw new NotImplementedException();
        }

        public Insumo ConsultarPorSigla(string siglaInsumo)
        {
            throw new NotImplementedException();
        }
    }
}
