
using ONS.WEBPMO.Domain.Repository.Base;

namespace ONS.WEBPMO.Domain.Repository
{
    
    public interface IOrigemColetaRepository : IRepository<OrigemColeta>
    {
        IList<OrigemColeta> ConsultarPorTipo(TipoOrigemColetaEnum tipo);
        IList<OrigemColeta> ConsultarPorIds(IList<string> ids);
        IList<UnidadeGeradora> ConsultarUnidadeGeradoraPorUsina(string idUsina);
        T FindByKey<T>(object key) where T : OrigemColeta;

        PagedResult<OrigemColeta> ConsultarOrigemColetasParticipamGabaritoPaginado(GabaritoParticipantesFilter filter);

        IList<OrigemColeta> ConsultarPorTipoNome(TipoOrigemColetaEnum tipo, string nome);
        IList<Usina> ConsultarUsinaParticipanteGabaritoPorColetaInsumo(int idColetaInsumo);
        IList<UnidadeGeradora> ConsultarUnidadeGeradoParticipanteGabaritoPorColetaInsumoUsina(
            int idColetaInsumo, string idUsina);

        IList<UnidadeGeradora> ConsultarUnidadeGeradoParticipanteGabaritoPorColetaInsumo(int idColetaInsumo);

        IList<UnidadeGeradoraManutencaoSGIDTO> ConsultarUnidadeGeradoParticipanteGabaritoPorColetaInsumos(List<int> idColetaInsumos);
    }
}
