using System.Diagnostics;
using Application.Clients.Commands;
using Application.Clients.DTO;
using Application.Clients.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CMSAPI.Areas.ClientApp.Controllers
{
    //[AllowAnonymous]
    [Area("Clients")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,LawyerAdmin")]
    public class ClientController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<ClientDTO>>> GetAllClients()
        {
            return await Mediator.Send(new GetAllClients.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetClientById(int id)
        {
            return HandleResult(await Mediator.Send(new GetClientById.Query { Id = id }));

        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateClient(CreateClientDTO createClientDto)
        {
            return await Mediator.Send(new CreateClient.Command { clientDto = createClientDto });
        }

        [HttpPut]
        public async Task<ActionResult> EditClient(EditClientDTO editClientDto)
        {
            return HandleResult(await Mediator.Send(new EditClient.Command { client = editClientDto }));

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClient(int id)
        {
            return HandleResult(await Mediator.Send(new DeleteClient.Command { Id = id }));
        }

    }
}
