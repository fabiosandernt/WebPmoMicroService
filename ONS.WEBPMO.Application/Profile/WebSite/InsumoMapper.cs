using AutoMapper;
using ONS.WEBPMO.Application.DTO;
using ONS.WEBPMO.Application.Models.Insumo;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Enumerations;

namespace ONS.WEBPMO.WebSite.AutoMapper
{
    public class InsumoMapper : Profile
    {
        public InsumoMapper()
        {
            CreateMap<Insumo, InsumoListagemModel>()
                  .ForMember(destino => destino.NomeTipoInsumo,
                             opt => opt.MapFrom(model => model.TipoInsumo == TipoInsumoEnum.Estruturado.ToString()
                                                                  ? TipoInsumoEnum.Estruturado.ToDescription()
                                                                  : TipoInsumoEnum.NaoEstruturado.ToDescription()))
                  .ForMember(destino => destino.ValorTipoInsumo, opt => opt.MapFrom(model => model.TipoInsumo))
                  .ForMember(destino => destino.PreAprovado,
                             opt => opt.MapFrom(model => model.PreAprovado ? "Sim" : "Não"))
                  .ForMember(destino => destino.SiglaInsumo, opt => opt.MapFrom(model => model.SiglaInsumo))
                  .ForMember(destino => destino.ExportarInsumo,
                             opt => opt.MapFrom(model => model.ExportarInsumo ? "Sim" : "Não"))
                  .ForMember(destino => destino.Ativo,
                             opt => opt.MapFrom(model => model.Ativo ? "Sim" : "Não"));

            CreateMap<InsumoNaoEstruturado, ConfiguracaoInsumoNaoEstruturado>()
                  .ForMember(destino => destino.IsBlocoMontador, opt => opt.MapFrom(insumo => insumo.IsUtilizadoDECOMP))
                  .ForMember(destino => destino.IsConvergenciaCCEE,
                             opt => opt.MapFrom(insumo => insumo.IsUtilizadoConvergencia))
                  .ForMember(destino => destino.IsPublicacaoResultados,
                             opt => opt.MapFrom(insumo => insumo.IsUtilizadoPublicacao))
                  .ForMember(destino => destino.IsProcessamentoPMO,
                             opt => opt.MapFrom(insumo => insumo.IsUtilizadoProcessamento))
                  .ForMember(destino => destino.IsPreAprovado, opt => opt.MapFrom(insumo => insumo.PreAprovado))
                  .ForMember(destino => destino.VersaoInsumoString, opt => opt.MapFrom(insumo => Convert.ToBase64String(insumo.Versao)))
                  .ForMember(destino => destino.TipoInsumo,
                             opt => opt.MapFrom(insumo => insumo.TipoInsumo == TipoInsumoEnum.Estruturado.ToString()
                                                              ? TipoInsumoEnum.Estruturado
                                                              : TipoInsumoEnum.NaoEstruturado));

            CreateMap<ConfiguracaoInsumoNaoEstruturado, InsumoNaoEstruturado>()
                  .ForMember(destino => destino.IsUtilizadoDECOMP, opt => opt.MapFrom(origem => origem.IsBlocoMontador))
                  .ForMember(destino => destino.IsUtilizadoConvergencia,
                             opt => opt.MapFrom(origem => origem.IsConvergenciaCCEE))
                  .ForMember(destino => destino.IsUtilizadoProcessamento,
                             opt => opt.MapFrom(origem => origem.IsProcessamentoPMO))
                  .ForMember(destino => destino.IsUtilizadoPublicacao,
                             opt => opt.MapFrom(origem => origem.IsPublicacaoResultados))
                  .ForMember(destino => destino.TipoInsumo,
                            opt => opt.MapFrom(origem => origem.TipoInsumo.ToString()))
                  .ForMember(destino => destino.PreAprovado,
                            opt => opt.MapFrom(origem => origem.IsPreAprovado))
                  .ForMember(destino => destino.Reservado,
                            opt => opt.MapFrom(origem => origem.Reservado.Equals("Sim") ? true : false));

            CreateMap<ManutencaoInsumoEstruturadoModel, InsumoEstruturado>()
                .ForMember(destino => destino.TipoInsumo,
                    opt => opt.MapFrom(origem => origem.TipoInsumo.ToString()));

            CreateMap<InsumoEstruturado, DadosManutencaoInsumoEstruturado>()
                .ForMember(destino => destino.TipoInsumo,
                    opt => opt.MapFrom(origem => origem.TipoInsumo == TipoInsumoEnum.Estruturado.ToString()
                        ? TipoInsumoEnum.Estruturado
                        : TipoInsumoEnum.NaoEstruturado));

            CreateMap<InsumoEstruturado, ManutencaoInsumoEstruturadoModel>()
                .ForMember(destino => destino.TipoInsumo,
                    opt => opt.MapFrom(origem => origem.TipoInsumo == TipoInsumoEnum.Estruturado.ToString()
                        ? TipoInsumoEnum.Estruturado.ToDescription()
                        : TipoInsumoEnum.NaoEstruturado.ToDescription()))
                .ForMember(destino => destino.IsPreAprovado, opt => opt.MapFrom(origem => origem.PreAprovado))
                .ForMember(destino => destino.CategoriaId, opt => opt.MapFrom(origem => origem.CategoriaInsumo.Id))
                .ForMember(destino => destino.TipoColetaId, opt => opt.MapFrom(origem => origem.TipoColeta.Id))
                .ForMember(destino => destino.ExportarInsumo, opt => opt.MapFrom(origem => origem.ExportarInsumo));

            CreateMap<InsumoEstruturado, VisualizarInsumoEstruturadoModel>()
                  .ForMember(destino => destino.PreAprovado,
                             opt => opt.MapFrom(origem => origem.PreAprovado ? "Sim" : "Não"))
                  .ForMember(destino => destino.Reservado,
                             opt => opt.MapFrom(origem => origem.Reservado ? "Sim" : "Não"))
                  .ForMember(destino => destino.TipoInsumo,
                             opt => opt.MapFrom(insumo => insumo.TipoInsumo == TipoInsumoEnum.Estruturado.ToString()
                                                              ? TipoInsumoEnum.Estruturado.ToDescription()
                                                             : TipoInsumoEnum.NaoEstruturado.ToDescription()))
                  .ForMember(destino => destino.ExportarInsumo,
                            opt => opt.MapFrom(origem => origem.ExportarInsumo ? "Sim" : "Não"))

                  .ForMember(destino => destino.Ativo,
                            opt => opt.MapFrom(origem => origem.Ativo ? "Sim" : "Não"));

            CreateMap<InsumoNaoEstruturado, VisualizarInsumoNaoEstruturadoModel>()
                 .ForMember(destino => destino.IsBlocoMontador, opt => opt.MapFrom(insumo => insumo.IsUtilizadoDECOMP))
                 .ForMember(destino => destino.IsConvergenciaCCEE,
                            opt => opt.MapFrom(insumo => insumo.IsUtilizadoConvergencia))
                 .ForMember(destino => destino.IsPublicacaoResultados,
                            opt => opt.MapFrom(insumo => insumo.IsUtilizadoPublicacao))
                 .ForMember(destino => destino.IsProcessamentoPMO,
                            opt => opt.MapFrom(insumo => insumo.IsUtilizadoProcessamento))
                 .ForMember(destino => destino.PreAprovado,
                             opt => opt.MapFrom(origem => origem.PreAprovado ? "Sim" : "Não"))
                 .ForMember(destino => destino.Reservado,
                             opt => opt.MapFrom(origem => origem.Reservado ? "Sim" : "Não"))
                 .ForMember(destino => destino.TipoInsumo,
                            opt => opt.MapFrom(insumo => insumo.TipoInsumo == TipoInsumoEnum.Estruturado.ToString()
                                                             ? TipoInsumoEnum.Estruturado.ToDescription()
                                                             : TipoInsumoEnum.NaoEstruturado.ToDescription()))
                .ForMember(destino => destino.ExportarInsumo,
                            opt => opt.MapFrom(origem => origem.ExportarInsumo ? "Sim" : "Não"));

            CreateMap<DadosInsumoConsultaDTO, InsumoConsultaModel>();

        }
    }
}