using Application.LawyerRequests.Commands;
using Application.LawyerRequests.DTO;
using Application.LawyerRequests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMSAPI.Areas.ClientApp.Controllers;

[Area("Clients")]
[Route("api/[area]/[controller]")]
[ApiController]
[Authorize(Roles = "Client")]
public class LawyerRequestsController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult> GetMyRequests()
    {
        return HandleResult(await Mediator.Send(new GetMyLawyerRequests.Query()));
    }

    [HttpPost]
    public async Task<ActionResult> CreateRequest(CreateLawyerRequestDto request)
    {
        return HandleResult(await Mediator.Send(new CreateLawyerRequest.Command { Request = request }));
    }
}

