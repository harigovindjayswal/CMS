using Application.LawyerAdmin.Commands;
using Application.LawyerAdmin.DTO;
using Application.LawyerAdmin.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMSAPI.Areas.LawyerAdminApp.Controllers;

[Area("LawyerAdmin")]
[Route("api/[area]/[controller]")]
[ApiController]
[Authorize(Roles = "LawyerAdmin")]
public class UsersController : BaseApiController
{
    [HttpGet("clients")]
    public async Task<ActionResult> GetMyClients()
    {
        return HandleResult(await Mediator.Send(new GetMyManagedClients.Query()));
    }

    [HttpGet("lawyers")]
    public async Task<ActionResult> GetMyLawyers()
    {
        return HandleResult(await Mediator.Send(new GetMyManagedLawyers.Query()));
    }

    [HttpPost("clients")]
    public async Task<ActionResult> CreateClient(CreateManagedClientDto dto)
    {
        return HandleResult(await Mediator.Send(new CreateManagedClient.Command { Client = dto }));
    }

    [HttpPost("lawyers")]
    public async Task<ActionResult> CreateLawyer(CreateManagedLawyerDto dto)
    {
        return HandleResult(await Mediator.Send(new CreateManagedLawyer.Command { Lawyer = dto }));
    }
}

