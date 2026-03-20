namespace Application.Documents.DTO;

public class DocumentFileDto
{
    public string FilePath { get; set; } = null!;
    public string ContentType { get; set; } = "application/octet-stream";
    public string DownloadName { get; set; } = "document";
}

