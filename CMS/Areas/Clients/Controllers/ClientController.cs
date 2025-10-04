using CMSRep.DbModels;
using CMSRep.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CMSAPI.Areas.Client.Controllers
{
    [Area("Clients")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {

        private readonly ICMSService _cmsService;
        public ClientController(ICMSService cmsService)
        {
            _cmsService = cmsService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            var result = await _cmsService.GetAllClient();

            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessage);
        }

        // GET: api/GetClientById/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(int id)
        {
            var result = await _cmsService.GetClientById(id);

            if (result.IsSuccess)
                return Ok(result.Data);

            return NotFound(result.ErrorMessage);
        }

        // POST: api/CreateClient
        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] ClientDTO clientDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _cmsService.CreateClient(clientDto);

            if (result.IsSuccess)
                return CreatedAtAction(nameof(GetClientById), new { id = result.Data.ClientId }, result.Data);

            return BadRequest(result.ErrorMessage);
        }

        // PUT: api/UpdateClient
        [HttpPut]
        public async Task<IActionResult> UpdateClient([FromBody] ClientDTO clientDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _cmsService.Update(clientDto);

            if (result.IsSuccess)
                return Ok("Client updated successfully.");

            return BadRequest(result.ErrorMessage);
        }

        // DELETE: api/DeleteClient
        
        [HttpDelete]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var result = await _cmsService.Delete(id);

            if (result.IsSuccess)
                return Ok("Client deleted successfully.");

            return BadRequest(result.ErrorMessage);
        }

    }
}
