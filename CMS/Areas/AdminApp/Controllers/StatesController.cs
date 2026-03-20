using Application.MasterData.States.Commands;
using Application.MasterData.States.DTO;
using Application.MasterData.States.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMSAPI.Areas.AdminApp.Controllers;

[Area("Admin")]
[Route("api/[area]/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class StatesController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<StateDto>>> GetStates()
    {
        return await Mediator.Send(new GetStates.Query());
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateState(StateDto state)
    {
        return await Mediator.Send(new CreateState.Command { State = state });
    }

    [HttpPut]
    public async Task<ActionResult> EditState(StateDto state)
    {
        return HandleResult(await Mediator.Send(new EditState.Command { State = state }));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteState(int id)
    {
        return HandleResult(await Mediator.Send(new DeleteState.Command { Id = id }));
    }
}

