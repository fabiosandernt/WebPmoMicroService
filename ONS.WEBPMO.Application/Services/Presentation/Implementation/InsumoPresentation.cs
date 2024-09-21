using ONS.WEBPMO.Application.DTO;
using ONS.WEBPMO.Domain.Enumerations;
using ONS.WEBPMO.Domain.Entities.PMO;
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

        public DadosManutencaoInsumoEstruturado ObterDadosManutencaoInsumoEstruturado(int? idInsumo)
        {
            DadosManutencaoInsumoEstruturado dto = new DadosManutencaoInsumoEstruturado();

            if (idInsumo.HasValue && idInsumo.Value > 0)
            {
                InsumoEstruturado insumo = (InsumoEstruturado)insumoRepository.FindByKey(idInsumo.Value);
                dto.Id = insumo.Id;
                dto.IsPreAprovado = insumo.PreAprovado;
                dto.Nome = insumo.Nome;
                dto.OrdemExibicao = insumo.OrdemExibicao;
                dto.IsReservado = insumo.Reservado;
                dto.TipoInsumo = insumo.TipoInsumo.Equals(TipoInsumoEnum.Estruturado.ToChar())
                    ? TipoInsumoEnum.Estruturado
                    : TipoInsumoEnum.NaoEstruturado;
                dto.CategoriaId = insumo.CategoriaInsumo.Id;
                dto.TipoColetaId = insumo.TipoColeta.Id;
                dto.VersaoStringInsumo = Convert.ToBase64String(insumo.Versao);
                dto.PermiteAlteracao = !insumo.Gabaritos.Any();
                dto.SiglaInsumo = insumo.SiglaInsumo;
                dto.ExportarInsumo = insumo.ExportarInsumo;
                dto.Ativo = insumo.Ativo;
            }

            var categorias = categoriaInsumoRepository.All().OrderBy(cat => cat.Id);
            var tiposColeta = tipoColetaRepository.All().OrderBy(tc => tc.Id);

            dto.Categorias.Add(new ChaveDescricaoDTO<int>(0, "Selecione..."));

            dto.Categorias = categorias.Select(a => new ChaveDescricaoDTO<int>(a.Id, a.Descricao)).ToList();
            dto.TiposColeta = tiposColeta.Select(i => new ChaveDescricaoDTO<int>(i.Id, i.Descricao)).ToList();

            return dto;
        }

        public DadosInsumoConsultaDTO ObterDadosInsumoConsulta()
        {
            DadosInsumoConsultaDTO dto = new DadosInsumoConsultaDTO();

            var categorias = categoriaInsumoRepository.All().OrderBy(cat => cat.Id);
            var tiposColeta = tipoColetaRepository.All().OrderBy(tc => tc.Id);

            dto.Categorias = categorias.Select(a => new ChaveDescricaoDTO<int>(a.Id, a.Descricao)).ToList();
            dto.TiposColeta = tiposColeta.Select(i => new ChaveDescricaoDTO<int>(i.Id, i.Descricao)).ToList();

            return dto;
        }
    }
}
