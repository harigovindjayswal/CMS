using Application.Cases.DTO;
using Application.Cases.Queries;
using Application.LawyerAdmin.DTO;
using Application.LawyerAdmin.Commands;
using Application.LawyerAdmin.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMSAPI.Areas.LawyerAdminApp.Controllers;

[Area("LawyerAdmin")]
[Route("api/[area]/[controller]")]
[ApiController]
[Authorize(Roles = "LawyerAdmin")]
public class CasesController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult> GetManagedCases()
    {
        return HandleResult(await Mediator.Send(new GetMyManagedCases.Query()));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetCase(int id)
    {
        // Reuse the same details query; it now supports LawyerAdmin access for managed cases.
        return HandleResult(await Mediator.Send(new GetCaseDetails.Query { Id = id }));
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateCaseDto dto)
    {
        return HandleResult(await Mediator.Send(new CreateCaseForRequest.Command { Case = dto }));
    }

    [HttpPost("manual")]
    public async Task<ActionResult> CreateManual(CreateManagedCaseDto dto)
    {
        return HandleResult(await Mediator.Send(new CreateManagedCase.Command { Case = dto }));
    }
}
