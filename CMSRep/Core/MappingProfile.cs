using AutoMapper;
using CMSApplication.Clients.DTO;
using CMSDb.DbModels;

namespace CMSAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Client, CreateClientDTO>().ReverseMap();
            CreateMap<Client, EditClientDTO>().ReverseMap();
            
        }
    }
}
