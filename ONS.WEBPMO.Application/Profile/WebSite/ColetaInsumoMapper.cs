using AutoMapper;
using ONS.WEBPMO.Application.DTO;
using ONS.WEBPMO.Application.Models;
using ONS.WEBPMO.Application.Models.ColetaInsumo;
using ONS.WEBPMO.Application.Models.GeracaoBlocos;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO;
using ONS.WEBPMO.Domain.Enumerations;

namespace ONS.WEBPMO.WebSite.AutoMapper
{
    public class ColetaInsumoMapper : Profile
    {
        public ColetaInsumoMapper()
        {
            // Mapping between DTOs and Models
            CreateMap<DadosPesquisaColetaInsumoDTO, PesquisaColetaInsumoModel>();
            CreateMap<DadosPesquisaColetaInsumoDTO, EnviarDadosColetaInsumoModel>();

            // Mapping between Domain Entity and Model
            CreateMap<ColetaInsumo, ColetaInsumoModel>()
                .ForMember(model => model.IdAgente, opt => opt.MapFrom(coleta => coleta.Agente.Id))
                .ForMember(model => model.IdColetaInsumo, opt => opt.MapFrom(coleta => coleta.Id))
                .ForMember(model => model.IdInsumo, opt => opt.MapFrom(coleta => coleta.Insumo.Id))
                .ForMember(model => model.IdSemanaOperativa, opt => opt.MapFrom(coleta => coleta.SemanaOperativa.Id))
                .ForMember(model => model.NomeAgente, opt => opt.MapFrom(coleta => coleta.NomeAgentePerfil))
                .ForMember(model => model.NomeInsumo, opt => opt.MapFrom(coleta => coleta.Insumo.Nome))
                .ForMember(model => model.NomeSemanaOperativa, opt => opt.MapFrom(coleta => coleta.SemanaOperativa.Nome))
                .ForMember(model => model.SituacaoColetaInsumo, opt => opt.MapFrom(coleta => coleta.Situacao.Id))
                .ForMember(model => model.SituacaoSemanaOperativa, opt => opt.MapFrom(coleta => coleta.SemanaOperativa.Situacao.Id))
                .ForMember(model => model.VersaoString, opt => opt.MapFrom(coleta => Convert.ToBase64String(coleta.Versao)))
                .ForMember(model => model.Versao, opt => opt.MapFrom(coleta => coleta.Versao))
                .ForMember(model => model.SituacaoColetaInsumoDescricao, opt => opt.MapFrom(coleta => coleta.Situacao.Descricao))
                .ForMember(model => model.IsSemanaPmo, opt => opt.MapFrom(coleta => coleta.SemanaOperativa.Revisao == 0))
                .ForMember(model => model.IsGeracaoComplementar, opt => opt.MapFrom(coleta =>
                 coleta.Insumo != null &&
                 coleta.Insumo.GetType() == typeof(InsumoEstruturado) &&
                 ((InsumoEstruturado)coleta.Insumo).TipoColeta.Id == (int)TipoColetaEnum.GeracaoComplementar))

                .ForMember(model => model.IsInsumoParaDECOMP, opt => opt.MapFrom(coleta => coleta.IsInsumoDECOMP))
                .ForMember(model => model.NomesGrandezasNaoEstagioAlteradas, opt => opt.MapFrom(coleta => coleta.NomesGrandezasNaoEstagioAlteradas));

            // Mapping for DadoColetaManutencao
            CreateMap<DadoColetaManutencao, PesquisaDadoColetaManutencaoModel>()
                .ForMember(model => model.IdDadoColeta, opt => opt.MapFrom(coleta => coleta.Id))
                .ForMember(model => model.NomeUnidade, opt => opt.MapFrom(coleta => coleta.Gabarito.OrigemColeta.Nome))
                .ForMember(model => model.NomeUsina, opt => opt.MapFrom(coleta => ((UnidadeGeradora)coleta.Gabarito.OrigemColeta).Usina.Nome));

            CreateMap<DadoColetaManutencao, AlteracaoDadoColetaManutencaoModel>()
                .ForMember(model => model.IdDadoColeta, opt => opt.MapFrom(coleta => coleta.Id))
                .ForMember(model => model.NomeUnidade, opt => opt.MapFrom(coleta => coleta.Gabarito.OrigemColeta.Nome))
                .ForMember(model => model.NomeUsina, opt => opt.MapFrom(coleta => ((UnidadeGeradora)coleta.Gabarito.OrigemColeta).Usina.Nome));

            CreateMap<DadoColetaManutencao, ImportacaoManutencaoModel>()
                .ForMember(model => model.IdColetaInsumo, opt => opt.MapFrom(coleta => coleta.ColetaInsumo.Id))
                .ForMember(model => model.IdUnidadeGeradora, opt => opt.MapFrom(coleta => coleta.UnidadeGeradora.Id))
                .ForMember(model => model.NomeUnidade, opt => opt.MapFrom(coleta => coleta.UnidadeGeradora.Nome))
                .ForMember(model => model.NomeUsina, opt => opt.MapFrom(coleta => coleta.UnidadeGeradora.Usina.Nome));

            // Mapping between DTO and Model with version conversion
            CreateMap<DadosAlteracaoDadoColetaManutencaoDTO, AlteracaoDadoColetaManutencaoModel>()
                .ForMember(model => model.VersaoColetaInsumo, opt => opt.MapFrom(dto => Convert.ToBase64String(dto.VersaoColetaInsumo)));

            // Mapping between Model and Filters with version conversion
            CreateMap<ColetaInsumoModel, EnviarDadosColetaInsumoManutencaoFilter>()
                .ForMember(filter => filter.Versao, opt => opt.MapFrom(model => Convert.FromBase64String(model.VersaoString)));

            CreateMap<ColetaInsumoModel, ColetaInsumoManutencaoFilter>()
                .ForMember(filter => filter.Versao, opt => opt.MapFrom(model => Convert.FromBase64String(model.VersaoString)));

            // Mapping between Models and DTOs with version conversion
            CreateMap<AlteracaoDadoColetaManutencaoModel, AlteracaoDadoColetaManutencaoDTO>()
                .ForMember(dto => dto.VersaoColetaInsumo, opt => opt.MapFrom(model => Convert.FromBase64String(model.VersaoColetaInsumo)));

            CreateMap<InclusaoDadoColetaManutencaoModel, InclusaoDadoColetaManutencaoDTO>()
                .ForMember(dto => dto.VersaoColetaInsumo, opt => opt.MapFrom(model => Convert.FromBase64String(model.VersaoColetaInsumo)));

            CreateMap<ExclusaoDadoColetaManutencaoModel, ExclusaoDadoColetaManutencaoDTO>()
                .ForMember(dto => dto.VersaoColetaInsumo, opt => opt.MapFrom(model => Convert.FromBase64String(model.VersaoColetaInsumo)));

            CreateMap<ImportacaoManutencaoModel, InclusaoDadoColetaManutencaoDTO>()
                .ForMember(dto => dto.VersaoColetaInsumo, opt => opt.MapFrom(model => Convert.FromBase64String(model.VersaoColetaInsumo)));

            // Mapping for ColetaInsumoNaoEstruturado
            CreateMap<DadoColetaNaoEstruturadoDTO, ColetaInsumoNaoEstruturadoModel>()
                .ForMember(model => model.IdAgente, opt => opt.MapFrom(coleta => coleta.IdAgente))
                .ForMember(model => model.IdColetaInsumo, opt => opt.MapFrom(coleta => coleta.IdColetaInsumo))
                .ForMember(model => model.IdInsumo, opt => opt.MapFrom(coleta => coleta.IdInsumo))
                .ForMember(model => model.IdSemanaOperativa, opt => opt.MapFrom(coleta => coleta.IdSemanaOperativa))
                .ForMember(model => model.NomeAgente, opt => opt.MapFrom(coleta => coleta.NomeAgente))
                .ForMember(model => model.NomeInsumo, opt => opt.MapFrom(coleta => coleta.NomeInsumo))
                .ForMember(model => model.NomeSemanaOperativa, opt => opt.MapFrom(coleta => coleta.NomeSemanaOperativa))
                .ForMember(model => model.SituacaoColetaInsumo, opt => opt.MapFrom(coleta => coleta.IdSituacaoColetaInsumo))
                .ForMember(model => model.SituacaoSemanaOperativa, opt => opt.MapFrom(coleta => coleta.IdSituacaoSemanaOperativa))
                .ForMember(model => model.VersaoString, opt => opt.MapFrom(coleta => Convert.ToBase64String(coleta.VersaoColetaInsumo)))
                .ForMember(model => model.Versao, opt => opt.MapFrom(coleta => coleta.VersaoColetaInsumo))
                .ForMember(model => model.IdDadoColetaNaoEstruturado, opt => opt.MapFrom(coleta => coleta.IdDadoColetaInsumo))
                .ForMember(model => model.MotivoAlteracaoColetaNaoEstruturado, opt => opt.MapFrom(coleta => coleta.MotivoAlteracaoONS))
                .ForMember(model => model.MotivoRejeicaoColetaNaoEstruturado, opt => opt.MapFrom(coleta => coleta.MotivoRejeicaoONS))
                .ForMember(model => model.ObservacaoColetaNaoEstruturada, opt => opt.MapFrom(coleta => coleta.Observacao))
                .ForMember(model => model.IsInsumoParaDECOMP, opt => opt.MapFrom(coleta => coleta.IsInsumoParaDECOMP));

            CreateMap<ColetaInsumo, ColetaInsumoNaoEstruturadoModel>()
                .ForMember(model => model.IdAgente, opt => opt.MapFrom(coleta => coleta.Agente.Id))
                .ForMember(model => model.IdColetaInsumo, opt => opt.MapFrom(coleta => coleta.Id))
                .ForMember(model => model.IdInsumo, opt => opt.MapFrom(coleta => coleta.Insumo.Id))
                .ForMember(model => model.IdSemanaOperativa, opt => opt.MapFrom(coleta => coleta.SemanaOperativa.Id))
                .ForMember(model => model.NomeAgente, opt => opt.MapFrom(coleta => coleta.NomeAgentePerfil))
                .ForMember(model => model.NomeInsumo, opt => opt.MapFrom(coleta => coleta.Insumo.Nome))
                .ForMember(model => model.NomeSemanaOperativa, opt => opt.MapFrom(coleta => coleta.SemanaOperativa.Nome))
                .ForMember(model => model.SituacaoColetaInsumo, opt => opt.MapFrom(coleta => coleta.Situacao.Id))
                .ForMember(model => model.SituacaoSemanaOperativa, opt => opt.MapFrom(coleta => coleta.SemanaOperativa.Situacao.Id))
                .ForMember(model => model.VersaoString, opt => opt.MapFrom(coleta => Convert.ToBase64String(coleta.Versao)))
                .ForMember(model => model.Versao, opt => opt.MapFrom(coleta => coleta.Versao))
                .ForMember(model => model.MotivoAlteracaoColetaNaoEstruturado, opt => opt.MapFrom(coleta => coleta.MotivoAlteracaoONS))
                .ForMember(model => model.MotivoRejeicaoColetaNaoEstruturado, opt => opt.MapFrom(coleta => coleta.MotivoRejeicaoONS))
                .ForMember(model => model.IsInsumoParaDECOMP, opt => opt.MapFrom(coleta => ((InsumoNaoEstruturado)coleta.Insumo).IsUtilizadoDECOMP));

            // Mapping for Arquivo and UploadFileModel
            CreateMap<Arquivo, UploadFileModel>()
                .ForMember(model => model.Name, opt => opt.MapFrom(obj => obj.Nome))
                .ForMember(model => model.Database, opt => opt.MapFrom(obj => false))
                .ForMember(model => model.TargetName, opt => opt.MapFrom(obj => UploadFileModel.PrefixDatabase + obj.Id))
                .ForMember(model => model.IdStoredIntoDatabase, opt => opt.MapFrom(obj => obj.Id))
                .ForMember(model => model.Size, opt => opt.MapFrom(obj => obj.Tamanho));

            CreateMap<ArquivoDadoNaoEstruturadoDTO, UploadFileModel>()
                .ForMember(model => model.Name, opt => opt.MapFrom(obj => obj.Nome))
                .ForMember(model => model.Database, opt => opt.MapFrom(obj => true))
                .ForMember(model => model.TargetName, opt => opt.MapFrom(obj => UploadFileModel.PrefixDatabase + obj.Id.ToString()))
                .ForMember(model => model.IdStoredIntoDatabase, opt => opt.MapFrom(obj => obj.Id))
                .ForMember(model => model.Size, opt => opt.MapFrom(obj => obj.Tamanho));

            CreateMap<UploadFileModel, ArquivoDadoNaoEstruturadoDTO>()
                .ForMember(destino => destino.Id, opt => opt.MapFrom(origem => origem.IdStoredIntoDatabase))
                .ForMember(destino => destino.Nome, opt => opt.MapFrom(origem => origem.Name))
                .ForMember(destino => destino.MimeType, opt => opt.MapFrom(origem => origem.MimeType))
                .ForMember(destino => destino.CaminhoFisicoCompleto, opt => opt.MapFrom(origem => origem.PhysicalFullPath))
                .ForMember(destino => destino.Tamanho, opt => opt.MapFrom(origem => origem.Size))
                .ForMember(destino => destino.IdArquivoTemporario, opt => opt.MapFrom(origem => origem.TargetName));

            // Mapping between Models and DTOs for ColetaInsumoNaoEstruturado
            CreateMap<ColetaInsumoNaoEstruturadoModel, DadosGravacaoDadoColetaInsumoNaoEstruturadoDTO>()
                .ForMember(destino => destino.IdColetaInsumo, opt => opt.MapFrom(origem => origem.IdColetaInsumo))
                .ForMember(destino => destino.IdAgente, opt => opt.MapFrom(origem => origem.IdAgente))
                .ForMember(destino => destino.IdInsumo, opt => opt.MapFrom(origem => origem.IdInsumo))
                .ForMember(destino => destino.Observacao, opt => opt.MapFrom(origem => origem.ObservacaoColetaNaoEstruturada))
                .ForMember(destino => destino.IdSemanaOperativa, opt => opt.MapFrom(origem => origem.IdSemanaOperativa ?? 0))
                .ForMember(destino => destino.IdDadoNaoEstruturado, opt => opt.MapFrom(origem => origem.IdDadoColetaNaoEstruturado))
                .ForMember(destino => destino.EnviarDadosAoSalvar, opt => opt.MapFrom(origem => origem.EnviarDadosAoSalvar))
                .ForMember(destino => destino.VersaoColetaInsumo, opt => opt.MapFrom(origem => origem.Versao))
                .ForMember(destino => destino.PreservarSituacaoDadoColeta, opt => opt.MapFrom(origem => true));

            CreateMap<ColetaInsumoNaoEstruturadoModel, DadosMonitoramentoColetaInsumoDTO>()
                .ForMember(destino => destino.IdColetaInsumo, opt => opt.MapFrom(origem => origem.IdColetaInsumo))
                .ForMember(destino => destino.MotivoAlteracaoONS, opt => opt.MapFrom(origem => origem.MotivoAlteracaoColetaNaoEstruturado))
                .ForMember(destino => destino.MotivoRejeicaoONS, opt => opt.MapFrom(origem => origem.MotivoRejeicaoColetaNaoEstruturado))
                .ForMember(destino => destino.VersaoColetaInsumo, opt => opt.MapFrom(origem => origem.Versao));

            // Additional Mappings
            CreateMap<PesquisaColetaInsumoModel, PesquisaGeracaoBlocosModel>();

            CreateMap<ValorDadoColetaModel, ValorDadoColetaDTO>()
                .ForMember(destino => destino.Valor, opt => opt.MapFrom(origem => origem.Valor ?? string.Empty));
        }
    }
}
