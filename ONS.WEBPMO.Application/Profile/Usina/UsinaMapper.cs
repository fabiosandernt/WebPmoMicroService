using ONS.WEBPMO.Domain.Enumerations;

namespace ONS.WEBPMO.Application.Profile.Usina
{
    public class UsinaMapper : AutoMapper.Profile
    {
        public UsinaMapper()
        {
            CreateMap<ONS.WEBPMO.Domain.Entities.BDT.UsinaPMO, ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO.Usina>()
                .ForMember(r => r.Id, opt => opt.MapFrom(rPmo => rPmo.Id))
                .ForMember(r => r.Nome, opt => opt.MapFrom(rPmo => rPmo.NomeExibicao))
                .ForMember(r => r.NomeCurto, opt => opt.MapFrom(rPmo => rPmo.NomeCurto))
                .ForMember(r => r.NomeLongo, opt => opt.MapFrom(rPmo => rPmo.NomeLongo))
                .ForMember(r => r.CodigoDPP, opt => opt.MapFrom(rPmo => rPmo.CodUsinaPlanejamento))
                .ForMember(r => r.IdSubsistema, opt => opt.MapFrom(rPmo => (rPmo.SiglaSubsistema ?? string.Empty).PadRight(2)))
                .ForMember(r => r.TipoUsina, opt => opt.MapFrom(rPmo =>
                    rPmo.TipoGeracao.StartsWith("H")
                        ? TipoUsinaEnum.Hidraulica.ToDescription()
                        : TipoUsinaEnum.Termica.ToDescription()))
                .ForMember(r => r.Cod_subsistemamodenerg, opt => opt.MapFrom(rPmo => rPmo.Cod_subsistemamodenerg))
                .ForMember(r => r.Id_reservatorioee, opt => opt.MapFrom(rPmo => rPmo.Id_reservatorioee))
                .ForMember(r => r.Cod_reservatorioee, opt => opt.MapFrom(rPmo => rPmo.Cod_reservatorioee))
                .ForMember(r => r.Nom_curto_reservatorioee, opt => opt.MapFrom(rPmo => rPmo.Nom_curto_reservatorioee))
                .ForMember(r => r.CodUsiPlanejamentoJusante, opt => opt.MapFrom(rPmo => rPmo.CodUsiPlanejamentoJusante))
                .ForMember(r => r.IdUsina, opt => opt.MapFrom(rPmo => rPmo.IdUsina))
                .ForMember(r => r.NomeCurtoSubmercado, opt => opt.MapFrom(rPmo => rPmo.NomeCurtoSubmercado))
                .ForMember(r => r.CodSubmercado, opt => opt.MapFrom(rPmo => rPmo.CodSubmercado))
                .ForMember(r => r.CodigoTipoGeracao, opt => opt.MapFrom(rPmo => rPmo.CodigoTipoGeracao));

            CreateMap<ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO.Usina, ONS.WEBPMO.Domain.Entities.Usina.OrigemColetaUsina.Usina>();
        }
    }
}
