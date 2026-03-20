using Application.MasterData.Courts.Commands;
using Application.MasterData.Courts.DTO;
using Application.MasterData.Courts.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMSAPI.Areas.AdminApp.Controllers;

[Area("Admin")]
[Route("api/[area]/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class CourtsController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<CourtDto>>> GetCourts([FromQuery] int? cityId)
    {
        return await Mediator.Send(new GetCourts.Query { CityId = cityId });
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateCourt(CourtDto court)
    {
        return await Mediator.Send(new CreateCourt.Command { Court = court });
    }

    [HttpPut]
    public async Task<ActionResult> EditCourt(CourtDto court)
    {
        return HandleResult(await Mediator.Send(new EditCourt.Command { Court = court }));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteCourt(int id)
    {
        return HandleResult(await Mediator.Send(new DeleteCourt.Command { Id = id }));
    }
}

