using Application.Clients.Commands;
using Application.Clients.DTO;
using Application.Clients.Validatators;
using FluentValidation;

namespace Application.Clients.Validatators; 
public class CreateClientValidator : BaseClientValidator<CreateClient.Command,CreateClientDTO>
{
    public CreateClientValidator():base(x=>x.clientDto)
    {   
      
    }
}