using Application.MasterData.CourtTypes.Commands;
using Application.MasterData.CourtTypes.DTO;
using Application.MasterData.CourtTypes.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMSAPI.Areas.AdminApp.Controllers;

[Area("Admin")]
[Route("api/[area]/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class CourtTypesController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<CourtTypeDto>>> GetCourtTypes()
    {
        return await Mediator.Send(new GetCourtTypes.Query());
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateCourtType(CourtTypeDto courtType)
    {
        return await Mediator.Send(new CreateCourtType.Command { CourtType = courtType });
    }

    [HttpPut]
    public async Task<ActionResult> EditCourtType(CourtTypeDto courtType)
    {
        return HandleResult(await Mediator.Send(new EditCourtType.Command { CourtType = courtType }));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteCourtType(int id)
    {
        return HandleResult(await Mediator.Send(new DeleteCourtType.Command { Id = id }));
    }
}

