using AutoMapper;
using ONS.WEBPMO.Application.DTO;
using ONS.WEBPMO.Application.Models.PLD;

namespace ONS.WEBPMO.WebSite.AutoMapper
{
    public class PLDMapper : Profile
    {
        public PLDMapper()
        {
            CreateMap<ArquivosSemanaOperativaConvergirPldDTO, ConvergirPLDModel>()
                .ForMember(destino => destino.Arquivos, opt => opt.MapFrom(origem => origem.Arquivos))
                .ForMember(destino => destino.DescricaoSemanaOperativa, opt => opt.MapFrom(origem => (origem.SemanaOperativa.Nome + " - " + origem.SemanaOperativa.Situacao.DscSituacaosemanaoper)))
                .ForMember(destino => destino.IdSemanaOperativa, opt => opt.MapFrom(origem => origem.SemanaOperativa.Id))
                .ForMember(destino => destino.VersaoStringSemanaOperativa, opt => opt.MapFrom(origem => Convert.ToBase64String(origem.SemanaOperativa.Versao)));
        }
    }
}