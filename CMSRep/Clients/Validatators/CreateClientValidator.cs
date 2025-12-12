using CMSApplication.Clients.Commands;
using CMSApplication.Clients.DTO;
using CMSApplication.Clients.Validatators;
using FluentValidation;

namespace CMSApplication.Clients.Validatators; 
public class CreateClientValidator : BaseClientValidator<CreateClient.Command,CreateClientDTO>
{
    public CreateClientValidator():base(x=>x.clientDto)
    {   
      
    }
}