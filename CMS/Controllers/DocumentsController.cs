using Application.Documents.Queries;
using Microsoft.AspNetCore.Mvc;

namespace CMSAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DocumentsController : BaseApiController
{
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Download(int id)
    {
        var result = await Mediator.Send(new GetDocumentFile.Query { DocumentId = id });
        if (!result.IsSuccess)
        {
            if (result.Code == 404) return NotFound();
            if (result.Code == 403) return Forbid();
            if (result.Code == 401) return Unauthorized();
            return BadRequest(result.Error);
        }

        var file = result.Value!;
        return PhysicalFile(file.FilePath, file.ContentType, file.DownloadName);
    }
}

