using Application.MasterData.Cities.Commands;
using Application.MasterData.Cities.DTO;
using Application.MasterData.Cities.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMSAPI.Areas.AdminApp.Controllers;

[Area("Admin")]
[Route("api/[area]/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class CitiesController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<CityDto>>> GetCities([FromQuery] int? districtId)
    {
        return await Mediator.Send(new GetCities.Query { DistrictId = districtId });
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateCity(CityDto city)
    {
        return await Mediator.Send(new CreateCity.Command { City = city });
    }

    [HttpPut]
    public async Task<ActionResult> EditCity(CityDto city)
    {
        return HandleResult(await Mediator.Send(new EditCity.Command { City = city }));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteCity(int id)
    {
        return HandleResult(await Mediator.Send(new DeleteCity.Command { Id = id }));
    }
}

