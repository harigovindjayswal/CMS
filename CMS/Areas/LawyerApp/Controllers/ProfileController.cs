using Application.Profile.Lawyers.Commands;
using Application.Profile.Lawyers.DTO;
using Application.Profile.Lawyers.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMSAPI.Areas.LawyerApp.Controllers;

[Area("Lawyer")]
[Route("api/[area]/[controller]")]
[ApiController]
[Authorize(Roles = "Lawyer")]
public class ProfileController : BaseApiController
{
    [HttpGet("Profile")]
    public async Task<ActionResult> GetMyProfile()
    {
        return HandleResult(await Mediator.Send(new GetMyLawyerProfile.Query()));
    }

    [HttpPut]
    public async Task<ActionResult> UpsertMyProfile(LawyerProfileDto profile)
    {
        return HandleResult(await Mediator.Send(new UpsertMyLawyerProfile.Command { Profile = profile }));
    }
}

