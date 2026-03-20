using Application.Cases.Commands;
using Application.Cases.DTO;
using Application.Cases.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMSAPI.Areas.LawyerApp.Controllers;

[Area("Lawyer")]
[Route("api/[area]/[controller]")]
[ApiController]
[Authorize(Roles = "Lawyer")]
public class CasesController : BaseApiController
{
    private readonly IWebHostEnvironment _env;

    public CasesController(IWebHostEnvironment env)
    {
        _env = env;
    }

    [HttpGet]
    public async Task<ActionResult> GetAssigned()
    {
        return HandleResult(await Mediator.Send(new GetAssignedCases.Query()));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetCase(int id)
    {
        return HandleResult(await Mediator.Send(new GetCaseDetails.Query { Id = id }));
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateCaseDto dto)
    {
        return HandleResult(await Mediator.Send(new CreateCase.Command { Case = dto }));
    }

    [HttpPut("status")]
    public async Task<ActionResult> UpdateStatus(UpdateCaseStatusDto dto)
    {
        return HandleResult(await Mediator.Send(new UpdateCaseStatus.Command { Status = dto }));
    }

    [HttpPost("notes")]
    public async Task<ActionResult> AddNote(AddNoteDto note)
    {
        return HandleResult(await Mediator.Send(new AddCaseNote.Command { Note = note }));
    }

    [HttpPost("{caseId:int}/documents")]
    public async Task<ActionResult> UploadDocument(
        int caseId,
        [FromForm] IFormFile file,
        [FromForm] string? title,
        [FromForm] string? category)
    {
        if (file == null || file.Length == 0) return BadRequest("File is required");

        const long maxBytes = 25 * 1024 * 1024; // 25 MB default for local/dev
        if (file.Length > maxBytes) return BadRequest("File is too large");

        var uploadsRoot = Path.Combine(_env.ContentRootPath, "Uploads", "cases", caseId.ToString());
        Directory.CreateDirectory(uploadsRoot);

        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        var allowed = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            ".pdf", ".png", ".jpg", ".jpeg", ".doc", ".docx", ".txt"
        };
        if (!allowed.Contains(ext))
            return BadRequest("File type is not allowed");

        var safeName = $"{Guid.NewGuid():N}{ext}";
        var fullPath = Path.Combine(uploadsRoot, safeName);

        await using (var stream = System.IO.File.Create(fullPath))
        {
            await file.CopyToAsync(stream);
        }

        return HandleResult(await Mediator.Send(new AddCaseDocument.Command
        {
            CaseId = caseId,
            Title = title,
            Category = category,
            FilePath = fullPath
        }));
    }
}
