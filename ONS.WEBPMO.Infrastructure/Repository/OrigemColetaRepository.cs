using ONS.Infra.Core.Pagination;
using ONS.WEBPMO.Domain.DTO;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO;
using ONS.WEBPMO.Domain.Enumerations;
using ONS.WEBPMO.Domain.Repository;
using ONS.WEBPMO.Infrastructure.Context;
using ONS.WEBPMO.Infrastructure.DataBase;

namespace ONS.WEBPMO.Domain.Repositories.Impl
{

    public class OrigemColetaRepository : Repository<OrigemColeta>, IOrigemColetaRepository
    {
        public OrigemColetaRepository(WEBPMODbContext context) : base(context)
        {
        }

        public PagedResult<OrigemColeta> ConsultarOrigemColetasParticipamGabaritoPaginado(GabaritoParticipantesFilter filter)
        {
            throw new NotImplementedException();
        }

        public IList<OrigemColeta> ConsultarPorIds(IList<string> ids)
        {
            throw new NotImplementedException();
        }

        public IList<OrigemColeta> ConsultarPorTipo(TipoOrigemColetaEnum tipo)
        {
            throw new NotImplementedException();
        }

        public IList<OrigemColeta> ConsultarPorTipoNome(TipoOrigemColetaEnum tipo, string nome)
        {
            throw new NotImplementedException();
        }

        public IList<UnidadeGeradora> ConsultarUnidadeGeradoParticipanteGabaritoPorColetaInsumo(int idColetaInsumo)
        {
            throw new NotImplementedException();
        }

        public IList<UnidadeGeradoraManutencaoSGIDTO> ConsultarUnidadeGeradoParticipanteGabaritoPorColetaInsumos(List<int> idColetaInsumos)
        {
            throw new NotImplementedException();
        }

        public IList<UnidadeGeradora> ConsultarUnidadeGeradoParticipanteGabaritoPorColetaInsumoUsina(int idColetaInsumo, string idUsina)
        {
            throw new NotImplementedException();
        }

        public IList<UnidadeGeradora> ConsultarUnidadeGeradoraPorUsina(string idUsina)
        {
            throw new NotImplementedException();
        }

        public IList<Usina> ConsultarUsinaParticipanteGabaritoPorColetaInsumo(int idColetaInsumo)
        {
            throw new NotImplementedException();
        }

        public T FindByKey<T>(object key) where T : OrigemColeta
        {
            throw new NotImplementedException();
        }
    }
}
