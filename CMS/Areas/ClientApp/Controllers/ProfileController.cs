using Application.Profile.Clients.Commands;
using Application.Profile.Clients.DTO;
using Application.Profile.Clients.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMSAPI.Areas.ClientApp.Controllers;

[Area("Clients")]
[Route("api/[area]/[controller]")]
[ApiController]
[Authorize(Roles = "Client")]
public class ProfileController : BaseApiController
{
    [HttpGet("Profile")]
    public async Task<ActionResult> GetMyProfile()
    {
        return HandleResult(await Mediator.Send(new GetMyClientProfile.Query()));
    }

    [HttpPut]
    public async Task<ActionResult> UpsertMyProfile(ClientProfileDto profile)
    {
        return HandleResult(await Mediator.Send(new UpsertMyClientProfile.Command { Profile = profile }));
    }
}

