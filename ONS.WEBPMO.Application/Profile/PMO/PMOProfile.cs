using ONS.WEBPMO.Application.DTO;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Application.Profile.PMO
{
    public class PMOProfile : AutoMapper.Profile
    {
        public PMOProfile()
        {
            CreateMap<Parametro, ParametroDTO>().ReverseMap(); 
        }
    }
}
