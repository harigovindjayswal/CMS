using Application.Common.Enums;
using Application.Utility.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMSAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class UtilityController : BaseApiController
    {

        [HttpGet("GetOptions")]
        public async Task<IActionResult> GetOptions([FromQuery] OptionType type)
        {
            return HandleResult(
                await Mediator.Send(new GetOtpionLoader.Query { Type = type }));
        }

    }
}
