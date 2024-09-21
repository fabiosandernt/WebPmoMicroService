using AutoMapper;
using ONS.SGIPMO.Domain.Entities;
using ONS.SGIPMO.Domain.Entities.Filters;
using ONS.SGIPMO.WebSite.Models;
using System.Collections.Generic;

namespace ONS.WEBPMO.WebSite.AutoMapper
{
    public class GabaritoMapper : Profile
    {
        protected override void Configure()
        {
            CreateMap<DadosFiltroPesquisaGabaritoDTO, GabaritoConsultaModel>();

            CreateMap<DadosConfiguracaoGabaritoDTO, ConfiguracaoGabaritoGeracaoComplementarModel>()
                .ForMember(model => model.IdSemanaOperativa, opt => opt.MapFrom(dto => dto.SemanaOperativa.Chave))
                .ForMember(model => model.IsPadrao, opt => opt.MapFrom(dto => dto.SemanaOperativa == null))
                .ForMember(model => model.NomeGabarito,
                    opt => opt.MapFrom(dto => dto.SemanaOperativa == null ? "Padrão" : dto.SemanaOperativa.Descricao));

            CreateMap<DadosConfiguracaoGabaritoDTO, ConfiguracaoGabaritoNaoEstruturadoModel>()
                .ForMember(model => model.IdSemanaOperativa, opt => opt.MapFrom(dto => dto.SemanaOperativa.Chave))
                .ForMember(model => model.IsPadrao, opt => opt.MapFrom(dto => dto.SemanaOperativa == null))
                .ForMember(model => model.NomeGabarito,
                    opt => opt.MapFrom(dto => dto.SemanaOperativa == null ? "Padrão" : dto.SemanaOperativa.Descricao));

            CreateMap<DadosConfiguracaoGabaritoDTO, ConfiguracaoGabaritoReservatorioModel>()
                .ForMember(model => model.IdSemanaOperativa, opt => opt.MapFrom(dto => dto.SemanaOperativa.Chave))
                .ForMember(model => model.IsPadrao, opt => opt.MapFrom(dto => dto.SemanaOperativa == null))
                .ForMember(model => model.NomeGabarito,
                    opt => opt.MapFrom(dto => dto.SemanaOperativa == null ? "Padrão" : dto.SemanaOperativa.Descricao));

            CreateMap<DadosConfiguracaoGabaritoDTO, ConfiguracaoGabaritoSubsistemaModel>()
                .ForMember(model => model.IdSemanaOperativa, opt => opt.MapFrom(dto => dto.SemanaOperativa.Chave))
                .ForMember(model => model.IsPadrao, opt => opt.MapFrom(dto => dto.SemanaOperativa == null))
                .ForMember(model => model.NomeGabarito,
                    opt => opt.MapFrom(dto => dto.SemanaOperativa == null ? "Padrão" : dto.SemanaOperativa.Descricao));

            CreateMap<DadosConfiguracaoGabaritoDTO, ConfiguracaoGabaritoUsinaModel>()
                .ForMember(model => model.IdSemanaOperativa, opt => opt.MapFrom(dto => dto.SemanaOperativa.Chave))
                .ForMember(model => model.IsPadrao, opt => opt.MapFrom(dto => dto.SemanaOperativa == null))
                .ForMember(model => model.NomeGabarito,
                    opt => opt.MapFrom(dto => dto.SemanaOperativa == null ? "Padrão" : dto.SemanaOperativa.Descricao));

            CreateMap<DadosConfiguracaoGabaritoDTO, ConfiguracaoGabaritoUnidadeGeradoraModel>()
                .ForMember(model => model.Usinas, opt => opt.MapFrom(dto => dto.OrigensColeta))
                .ForMember(model => model.OrigensColeta, opt => opt.Ignore())
                .ForMember(model => model.NomeGabarito,
                    opt => opt.MapFrom(dto => dto.SemanaOperativa == null ? "Padrão" : dto.SemanaOperativa.Descricao));

            CreateMap<DadosConfiguracaoGabaritoUnidadeGeradoraDTO, ConfiguracaoGabaritoUnidadeGeradoraModel>()
                .ForMember(model => model.Usinas, opt => opt.MapFrom(dto => dto.OrigensColeta))
                .ForMember(model => model.OrigensColeta, opt => opt.MapFrom(dto => dto.UnidadesGeradoras))
                .ForMember(model => model.NomeGabarito,
                    opt => opt.MapFrom(dto => dto.SemanaOperativa == null ? "Padrão" : dto.SemanaOperativa.Descricao));

            CreateMap<DadosManutencaoGabaritoDTO, ManutencaoGabaritoReservatorioModel>()
                .ForMember(model => model.IdAgente, opt => opt.MapFrom(dto => dto.Agente.Chave))
                .ForMember(model => model.IdOrigemColeta, opt => opt.MapFrom(dto => dto.OrigemColeta.Chave))
                .ForMember(model => model.IdSemanaOperativa, opt => opt.MapFrom(dto => dto.SemanaOperativa.Chave))
                .ForMember(model => model.NomeAgente, opt => opt.MapFrom(dto => dto.Agente.Descricao))
                .ForMember(model => model.NomeOrigemColeta, opt => opt.MapFrom(dto => dto.OrigemColeta.Descricao))
                .ForMember(model => model.NomeGabarito, opt => opt.MapFrom(dto => dto.SemanaOperativa == null
                    ? "Padrão"
                    : dto.SemanaOperativa.Descricao));

            CreateMap<DadosManutencaoGabaritoDTO, ManutencaoGabaritoUsinaModel>()
                .ForMember(model => model.IdAgente, opt => opt.MapFrom(dto => dto.Agente.Chave))
                .ForMember(model => model.IdOrigemColeta, opt => opt.MapFrom(dto => dto.OrigemColeta.Chave))
                .ForMember(model => model.IdSemanaOperativa, opt => opt.MapFrom(dto => dto.SemanaOperativa.Chave))
                .ForMember(model => model.NomeAgente, opt => opt.MapFrom(dto => dto.Agente.Descricao))
                .ForMember(model => model.NomeOrigemColeta, opt => opt.MapFrom(dto => dto.OrigemColeta.Descricao))
                .ForMember(model => model.NomeGabarito, opt => opt.MapFrom(dto => dto.SemanaOperativa == null
                    ? "Padrão"
                    : dto.SemanaOperativa.Descricao));

            CreateMap<DadosManutencaoGabaritoDTO, ManutencaoGabaritoUnidadeGeradoraModel>()
                .ForMember(model => model.IdAgente, opt => opt.MapFrom(dto => dto.Agente.Chave))
                .ForMember(model => model.IdOrigemColeta, opt => opt.MapFrom(dto => dto.OrigemColeta.Chave))
                .ForMember(model => model.IdSemanaOperativa, opt => opt.MapFrom(dto => dto.SemanaOperativa.Chave))
                .ForMember(model => model.NomeAgente, opt => opt.MapFrom(dto => dto.Agente.Descricao))
                .ForMember(model => model.NomeOrigemColeta, opt => opt.MapFrom(dto => dto.OrigemColeta.Descricao))
                .ForMember(model => model.NomeGabarito, opt => opt.MapFrom(dto => dto.SemanaOperativa == null
                    ? "Padrão"
                    : dto.SemanaOperativa.Descricao));

            CreateMap<DadosManutencaoGabaritoDTO, ManutencaoGabaritoSubsistemaModel>()
                .ForMember(model => model.IdAgente, opt => opt.MapFrom(dto => dto.Agente.Chave))
                .ForMember(model => model.IdOrigemColeta, opt => opt.MapFrom(dto => dto.OrigemColeta.Chave))
                .ForMember(model => model.IdSemanaOperativa, opt => opt.MapFrom(dto => dto.SemanaOperativa.Chave))
                .ForMember(model => model.NomeAgente, opt => opt.MapFrom(dto => dto.Agente.Descricao))
                .ForMember(model => model.NomeOrigemColeta, opt => opt.MapFrom(dto => dto.OrigemColeta.Descricao))
                .ForMember(model => model.NomeGabarito, opt => opt.MapFrom(dto => dto.SemanaOperativa == null
                    ? "Padrão"
                    : dto.SemanaOperativa.Descricao));

            CreateMap<DadosManutencaoGabaritoDTO, ManutencaoGabaritoGeracaoComplementarModel>()
                .ForMember(model => model.IdAgente, opt => opt.MapFrom(dto => dto.Agente.Chave))
                .ForMember(model => model.IdSemanaOperativa, opt => opt.MapFrom(dto => dto.SemanaOperativa.Chave))
                .ForMember(model => model.NomeAgente, opt => opt.MapFrom(dto => dto.Agente.Descricao))
                .ForMember(model => model.NomeGabarito, opt => opt.MapFrom(dto => dto.SemanaOperativa == null
                    ? "Padrão"
                    : dto.SemanaOperativa.Descricao));

            CreateMap<DadosManutencaoGabaritoDTO, ManutencaoGabaritoNaoEstruturadoModel>()
                .ForMember(model => model.IdAgente, opt => opt.MapFrom(dto => dto.Agente.Chave))
                .ForMember(model => model.IdSemanaOperativa, opt => opt.MapFrom(dto => dto.SemanaOperativa.Chave))
                .ForMember(model => model.NomeAgente, opt => opt.MapFrom(dto => dto.Agente.Descricao))
                .ForMember(model => model.NomeGabarito, opt => opt.MapFrom(dto => dto.SemanaOperativa == null
                    ? "Padrão"
                    : dto.SemanaOperativa.Descricao));

            CreateMap<BaseGabaritoModel, GabaritoConfiguracaoDTO>();
            CreateMap<ConfiguracaoGabaritoGeracaoComplementarModel, GabaritoConfiguracaoDTO>();
            CreateMap<ConfiguracaoGabaritoReservatorioModel, GabaritoConfiguracaoDTO>()
                .ForMember(dto => dto.IdsOrigemColeta,
                           opt => opt.ResolveUsing(model => model.IdOrigemColeta != null ? new List<string> { model.IdOrigemColeta } : new List<string>()));
            CreateMap<ConfiguracaoGabaritoSubsistemaModel, GabaritoConfiguracaoDTO>()
                .ForMember(dto => dto.IdsOrigemColeta,
                           opt => opt.ResolveUsing(model => model.IdOrigemColeta != null ? new List<string> { model.IdOrigemColeta } : new List<string>()));
            CreateMap<ConfiguracaoGabaritoUnidadeGeradoraModel, GabaritoConfiguracaoDTO>()
                .ForMember(dto => dto.IdsOrigemColeta, opt => opt.MapFrom(model => model.IdsOrigemColeta))
                .ForMember(dto => dto.IdOrigemColetaPai, opt => opt.MapFrom(model => model.IdUsina));
            CreateMap<ConfiguracaoGabaritoUsinaModel, GabaritoConfiguracaoDTO>()
                .ForMember(dto => dto.IdsOrigemColeta,
                           opt => opt.ResolveUsing(model => model.IdOrigemColeta != null ? new List<string> { model.IdOrigemColeta } : new List<string>()));

            CreateMap<ManutencaoGabaritoModel, GabaritoConfiguracaoDTO>()
                .ForMember(dto => dto.IdsOrigemColeta,
                           opt => opt.ResolveUsing(model => model.IdOrigemColeta != null ? new List<string> { model.IdOrigemColeta } : new List<string>()));
            CreateMap<ManutencaoGabaritoUnidadeGeradoraModel, GabaritoConfiguracaoDTO>()
                .ForMember(dto => dto.IdsOrigemColeta, opt => opt.MapFrom(model => model.IdsOrigemColeta))
                .ForMember(dto => dto.IdOrigemColetaPai, opt => opt.MapFrom(model => model.IdOrigemColeta));

            CreateMap<DadosManutencaoGabaritoDTO, VisualizarGabaritoModel>()
                .ForMember(dto => dto.NomeAgente, opt => opt.MapFrom(model => model.Agente.Descricao))
                .ForMember(dto => dto.NomeOrigemColeta, opt => opt.MapFrom(model => model.OrigemColeta.Descricao))
                .ForMember(dto => dto.NomeGabarito,
                    opt =>
                        opt.MapFrom(model => model.SemanaOperativa == null ? "Padrão" : model.SemanaOperativa.Descricao));

            CreateMap<ConfiguracaoGabaritoUnidadeGeradoraModel, GabaritoDadosFilter>()
                .ForMember(filter => filter.IdOrigemColeta, opt => opt.MapFrom(model => model.IdUsina));

            base.Configure();
        }
    }
}