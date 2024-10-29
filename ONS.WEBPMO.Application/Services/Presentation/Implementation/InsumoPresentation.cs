using ONS.WEBPMO.Application.DTO;
using ONS.WEBPMO.Domain.Repository;

namespace ONS.WEBPMO.Domain.Presentations.Impl
{
    public class InsumoPresentation : IInsumoPresentation
    {
        private readonly ICategoriaInsumoRepository categoriaInsumoRepository;
        private readonly ITipoColetaRepository tipoColetaRepository;
        private readonly IInsumoRepository insumoRepository;

        public InsumoPresentation(
            ICategoriaInsumoRepository categoriaInsumoRepository,
            ITipoColetaRepository tipoColetaRepository,
            IInsumoRepository insumoRepository)
        {
            this.categoriaInsumoRepository = categoriaInsumoRepository;
            this.tipoColetaRepository = tipoColetaRepository;
            this.insumoRepository = insumoRepository;
        }

        public DadosInsumoConsultaDTO ObterDadosInsumoConsulta()
        {
            throw new NotImplementedException();
        }

        public DadosManutencaoInsumoEstruturado ObterDadosManutencaoInsumoEstruturado(int? idInsumo)
        {
            throw new NotImplementedException();
        }
    }
}
