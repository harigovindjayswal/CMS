using Application.MasterData.CaseTypes.Commands;
using Application.MasterData.CaseTypes.DTO;
using Application.MasterData.CaseTypes.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMSAPI.Areas.AdminApp.Controllers;

[Area("Admin")]
[Route("api/[area]/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class CaseTypesController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<CaseTypeDto>>> GetCaseTypes()
    {
        return await Mediator.Send(new GetCaseTypes.Query());
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateCaseType(CaseTypeDto caseType)
    {
        return await Mediator.Send(new CreateCaseType.Command { CaseType = caseType });
    }

    [HttpPut]
    public async Task<ActionResult> EditCaseType(CaseTypeDto caseType)
    {
        return HandleResult(await Mediator.Send(new EditCaseType.Command { CaseType = caseType }));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteCaseType(int id)
    {
        return HandleResult(await Mediator.Send(new DeleteCaseType.Command { Id = id }));
    }
}

