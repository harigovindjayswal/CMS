using Application.Clients.DTO;
using AutoMapper;
using Domain.AppEntities;


namespace Application.Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateClientDTO, Client>();
            CreateMap<EditClientDTO, Client>();
            CreateMap<Client, ClientDTO>();

        }
    }
}
