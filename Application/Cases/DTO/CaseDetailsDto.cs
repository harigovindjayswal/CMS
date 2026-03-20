using System.Collections.Generic;

namespace Application.Cases.DTO;

public class CaseDetailsDto : CaseDto
{
    public List<NoteDto> Notes { get; set; } = [];
    public List<DocumentDto> Documents { get; set; } = [];
}

