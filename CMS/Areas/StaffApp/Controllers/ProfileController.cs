using Application.Profile.Staff.Commands;
using Application.Profile.Staff.DTO;
using Application.Profile.Staff.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMSAPI.Areas.StaffApp.Controllers;

[Area("Staff")]
[Route("api/[area]/[controller]")]
[ApiController]
[Authorize(Roles = "Staff")]
public class ProfileController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult> Get()
    {
        return HandleResult(await Mediator.Send(new GetMyStaffProfile.Query()));
    }

    [HttpPut]
    public async Task<ActionResult> Upsert(StaffProfileDto dto)
    {
        return HandleResult(await Mediator.Send(new UpsertMyStaffProfile.Command { Profile = dto }));
    }
}

