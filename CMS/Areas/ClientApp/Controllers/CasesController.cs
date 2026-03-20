using Application.Cases.Commands;
using Application.Cases.DTO;
using Application.Cases.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMSAPI.Areas.ClientApp.Controllers;

[Area("Clients")]
[Route("api/[area]/[controller]")]
[ApiController]
[Authorize(Roles = "Client")]
public class CasesController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult> GetMyCases()
    {
        return HandleResult(await Mediator.Send(new GetMyCases.Query()));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetCase(int id)
    {
        return HandleResult(await Mediator.Send(new GetCaseDetails.Query { Id = id }));
    }

    [HttpPost("notes")]
    public async Task<ActionResult> AddNote(AddNoteDto note)
    {
        return HandleResult(await Mediator.Send(new AddCaseNote.Command { Note = note }));
    }
}

