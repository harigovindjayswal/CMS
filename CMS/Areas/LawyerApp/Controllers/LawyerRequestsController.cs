using Application.LawyerRequests.Commands;
using Application.LawyerRequests.DTO;
using Application.LawyerRequests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMSAPI.Areas.LawyerApp.Controllers;

[Area("Lawyer")]
[Route("api/[area]/[controller]")]
[ApiController]
[Authorize(Roles = "Lawyer")]
public class LawyerRequestsController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult> GetIncoming()
    {
        return HandleResult(await Mediator.Send(new GetIncomingLawyerRequests.Query()));
    }

    [HttpPut("respond")]
    public async Task<ActionResult> Respond(RespondLawyerRequestDto response)
    {
        return HandleResult(await Mediator.Send(new RespondToLawyerRequest.Command { Response = response }));
    }
}

