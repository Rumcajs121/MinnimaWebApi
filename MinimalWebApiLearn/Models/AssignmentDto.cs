using System.ComponentModel.DataAnnotations;

namespace MinimalWebApiLearn;

public class AssignmentDto
{
    public required string Description { get; set; }
    public required string EndDate {get; set;}
}
