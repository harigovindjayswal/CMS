using AutoMapper;
using CMSDb.DbModels;
using CMSRep.DbModels;

namespace CMSAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Client, ClientDTO>().ReverseMap();
            
        }
    }
}
