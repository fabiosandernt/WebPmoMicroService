using ONS.WEBPMO.Application.DTO;
using ONS.WEBPMO.Application.Dtos;
using ONS.WEBPMO.Application.Models.Insumo;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Enumerations;

namespace ONS.WEBPMO.Application.Services.PMO.Interfaces
{

    public interface IInsumoService
    {
        ICollection<VisualizarInsumoModel> GetByQueryableAsync(InsumoFiltro filter);

        Task<IList<VisualizarInsumoModel>> ConsultarTodosInsumosAsync();

        Task<IList<Insumo>> ConsultarInsumosNaoEstruturadoEEstruturadoComGrandezaAtivaAsync();

        Task<IList<InsumoEstruturado>> ConsultarInsumoPorTipoOrigemColetaCategoriaAsync(TipoOrigemColetaEnum tipoOrigemColeta, CategoriaInsumoEnum? categoria = null);

        Task<IList<InsumoEstruturado>> ConsultarInsumoEstruturadosPorUsinaAsync(string idUsina);

        Task<IList<Insumo>> ConsultarInsumosPorNomeAsync(string nomeInsumo);

        Task<IList<InsumoNaoEstruturado>> ConsultarInsumoNaoEstruturadoAsync();

        Task<ICollection<Insumo>> ConsultarInsumosPorFiltroAsync(InsumoFiltro filtro);

        Task<int> InserirInsumoNaoEstruturadoAsync(InsumoNaoEstruturado insumo);

        Task<InsumoNaoEstruturado> ObterInsumoNaoEstruturadoPorChaveAsync(int id);

        Task<int> InserirInsumoEstruturadoAsync(DadosInclusaoInsumoEstruturadoDTO insumo);

        Task<InsumoEstruturado> ObterInsumoEstruturadoPorChaveAsync(int id);

        Task AlterarInsumoNaoEstruturadoAsync(InsumoNaoEstruturado insumo, byte[] versao);

        Task AlterarInsumoEstruturadoAsync(DadosInclusaoInsumoEstruturadoDTO dadosInsumoEstruturado);

        Task ExcluirInsumoPorChaveAsync(int idInsumo, byte[] versao);

        Task<bool> InsumoBloqueadosAlteracaoAsync(int id);

        Task VerificarInsumoReservadoAsync(int id);

        Task<Grandeza> ObterGrandezaPorIdAsync(int idGrandeza);

        Task ValidarIncluirAlterarGrandezaAsync(Grandeza grandeza);

        Task ValidarExclusaoGrandezaAsync(int idGrandeza); // Já estava assíncrono

        Task<IList<TipoDadoGrandeza>> ObterTiposDadoGrandezaAsync();

        Task<IList<Grandeza>> ObterGrandezasPorInsumoAsync(int idInsumo);

        Task<IList<Insumo>> ConsultarInsumosPorSemanaOperativaAgentesAsync(int idSemanaOperativa, params int[] idsAgente);

        Task<bool> PermitirAlteracaoGrandezaAsync(int idGrandeza);

        Task<InsumoDto> ConsultarInsumoAsync(int id);
    }
}


