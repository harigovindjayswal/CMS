using Application.MasterData.Districts.Commands;
using Application.MasterData.Districts.DTO;
using Application.MasterData.Districts.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMSAPI.Areas.AdminApp.Controllers;

[Area("Admin")]
[Route("api/[area]/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class DistrictsController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<DistrictDto>>> GetDistricts([FromQuery] int? stateId)
    {
        return await Mediator.Send(new GetDistricts.Query { StateId = stateId });
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateDistrict(DistrictDto district)
    {
        return await Mediator.Send(new CreateDistrict.Command { District = district });
    }

    [HttpPut]
    public async Task<ActionResult> EditDistrict(DistrictDto district)
    {
        return HandleResult(await Mediator.Send(new EditDistrict.Command { District = district }));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteDistrict(int id)
    {
        return HandleResult(await Mediator.Send(new DeleteDistrict.Command { Id = id }));
    }
}

