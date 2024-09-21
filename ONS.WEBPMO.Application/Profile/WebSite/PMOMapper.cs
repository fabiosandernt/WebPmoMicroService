using AutoMapper;

namespace ONS.WEBPMO.WebSite.AutoMapper
{
    public class PMOMapper : Profile
    {
        protected override void Configure()
        {
            CreateMap<PMO, PMOManterModel>();
            CreateMap<SemanaOperativa, SemanaOperativaModel>();
            CreateMap<SemanaOperativa, AlteracaoSemanaOperativaModel>();
            CreateMap<AlteracaoSemanaOperativaModel, DadosAlteracaoSemanaOperativaDTO>();
            CreateMap<InclusaoSemanaOperativaModel, InclusaoSemanaOperativaDTO>();

            CreateMap<EscolhaGabaritoModel, ResetGabaritoDTO>()
                .ForMember(destino => destino.IdSemanaOperativa, opt => opt.MapFrom(origem => origem.IdSemanaOperativa))
                .ForMember(destino => destino.IdEstudo, opt => opt.MapFrom(origem => origem.IdEstudo))
                .ForMember(destino => destino.IsPadrao, opt => opt.MapFrom(origem => origem.IsPadrao))
                .ForMember(destino => destino.VersaoPMO, opt => opt.MapFrom(origem => origem.VersaoPMO))
                .ForMember(destino => destino.VersaoSemanaOperativa, opt => opt.MapFrom(origem => origem.VersaoSemanaOperativa));
        }
    }
}