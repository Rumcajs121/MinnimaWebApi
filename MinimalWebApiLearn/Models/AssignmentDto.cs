using System.ComponentModel.DataAnnotations;

namespace MinimalWebApiLearn;

public class AssignmentDto
{
    public required string Description { get; set; }
    [DataType(DataType.Date)]
    public DateTime EndDate {get; set;}
}
