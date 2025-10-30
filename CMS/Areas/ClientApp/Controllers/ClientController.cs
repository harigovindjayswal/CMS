using CMSApplication.Clients.Commands;
using CMSApplication.Clients.Queries;
using CMSDb.DbModels;
using CMSRep.DbModels;
using CMSRep.IServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Diagnostics;

namespace CMSAPI.Areas.ClientApp.Controllers
{
    [Area("Clients")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private IMediator? _mediator;

        protected IMediator Mediator =>
            _mediator ??= HttpContext.RequestServices.GetService<IMediator>()
                ?? throw new InvalidOperationException("IMediator service is unavailable");

        [HttpGet]
        public async Task<ActionResult<List<Client>>> GetAllClients()
        {
            return await Mediator.Send(new GetAllClients.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClientById(int id)
        {
            return await Mediator.Send(new GetClientById.Query { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateClient(ClientDTO Client)
        {
            return await Mediator.Send(new CreateClient.Command { clientDto = Client });
        }

        [HttpPut]
        public async Task<ActionResult<int>> EditClient(ClientDTO Client)
        {
            var res= await Mediator.Send(new EditClient.Command { client = Client });
            return res;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteClient(int id)
        {
           return await Mediator.Send(new DeleteClient.Command { Id = id });
        }

        //private readonly ICMSService _cmsService;
        //public ClientController(ICMSService cmsService)
        //{
        //    _cmsService = cmsService;
        //}
        //[HttpGet]
        //public async Task<IActionResult> GetAllClients()
        //{
        //    var result = await _cmsService.GetAllClient();

        //    if (result.IsSuccess)
        //        return Ok(result.Data);

        //    return BadRequest(result.ErrorMessage);
        //}

        //// GET: api/GetClientById/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetClientById(int id)
        //{
        //    var result = await _cmsService.GetClientById(id);

        //    if (result.IsSuccess)
        //        return Ok(result.Data);

        //    return NotFound(result.ErrorMessage);
        //}

        //// POST: api/CreateClient
        //[HttpPost]
        //public async Task<IActionResult> CreateClient([FromBody] ClientDTO clientDto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var result = await _cmsService.CreateClient(clientDto);

        //    if (result.IsSuccess)
        //        return CreatedAtAction(nameof(GetClientById), new { id = result.Data.ClientId }, result.Data);

        //    return BadRequest(result.ErrorMessage);
        //}

        //// PUT: api/UpdateClient
        //[HttpPut]
        //public async Task<IActionResult> UpdateClient([FromBody] ClientDTO clientDto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var result = await _cmsService.Update(clientDto);

        //    if (result.IsSuccess)
        //        return Ok("Client updated successfully.");

        //    return BadRequest(result.ErrorMessage);
        //}

        //// DELETE: api/DeleteClient

        //[HttpDelete]
        //public async Task<IActionResult> DeleteClient(int id)
        //{
        //    var result = await _cmsService.Delete(id);

        //    if (result.IsSuccess)
        //        return Ok("Client deleted successfully.");

        //    return BadRequest(result.ErrorMessage);
        //}

    }
}
