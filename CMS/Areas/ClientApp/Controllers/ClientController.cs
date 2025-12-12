using System.Diagnostics;
using CMSApplication.Clients.Commands;
using CMSApplication.Clients.DTO;
using CMSApplication.Clients.Queries;
using CMSDb.DbModels;
using CMSRep.IServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CMSAPI.Areas.ClientApp.Controllers
{
    [Area("Clients")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class ClientController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<Client>>> GetAllClients()
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
