using AutoMapper;

namespace ONS.WEBPMO.WebSite.AutoMapper
{
    public class PLDMapper : Profile
    {
        protected override void Configure()
        {
            CreateMap<ArquivosSemanaOperativaConvergirPldDTO, ConvergirPLDModel>()
                .ForMember(destino => destino.Arquivos, opt => opt.MapFrom(origem => origem.Arquivos))
                .ForMember(destino => destino.DescricaoSemanaOperativa, opt => opt.MapFrom(origem => (origem.SemanaOperativa.Nome + " - " + origem.SemanaOperativa.Situacao.Descricao)))
                .ForMember(destino => destino.IdSemanaOperativa, opt => opt.MapFrom(origem => origem.SemanaOperativa.Id))
                .ForMember(destino => destino.VersaoStringSemanaOperativa, opt => opt.ResolveUsing(origem => Convert.ToBase64String(origem.SemanaOperativa.Versao)));
        }
    }
}