﻿
using ONS.WEBPMO.Domain.Entities.BDT;

namespace ONS.WEBPMO.Application.Profile.Usina
{
    public class ReservatorioMapper : AutoMapper.Profile
    {
        public ReservatorioMapper()
        {
            CreateMap<ReservatorioPMO, ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO.Reservatorio>()
               .ForMember(r => r.Id, opt => opt.MapFrom(rPmo => rPmo.Id))
               .ForMember(r => r.Nome, opt => opt.MapFrom(rPmo => rPmo.NomeExibicao))
               .ForMember(r => r.NomeCurto, opt => opt.MapFrom(rPmo => rPmo.NomeCurto))
               .ForMember(r => r.NomeLongo, opt => opt.MapFrom(rPmo => rPmo.NomeLongo))
               .ForMember(r => r.CodigoDPP, opt => opt.MapFrom(rPmo => rPmo.Codigo))
               .ForMember(r => r.IdSubsistema, opt => opt.MapFrom(rPmo => (rPmo.SiglaSubsistema ?? string.Empty).PadRight(2)))
               .ForMember(r => r.Cod_subsistemamodenerg, opt => opt.MapFrom(rPmo => rPmo.Cod_subsistemamodenerg))
                .ForMember(r => r.Cod_reservatorioee, opt => opt.MapFrom(rPmo => rPmo.Cod_reservatorioee))
                .ForMember(r => r.Nom_curto_reservatorioee, opt => opt.MapFrom(rPmo => rPmo.Nom_curto_reservatorioee))
                .ForMember(r => r.CodUsiPlanejamentoJusante, opt => opt.MapFrom(rPmo => rPmo.CodUsiPlanejamentoJusante));

            CreateMap<ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO.Reservatorio, ONS.WEBPMO.Domain.Entities.Usina.OrigemColetaUsina.Reservatorio>();

        }
    }
}