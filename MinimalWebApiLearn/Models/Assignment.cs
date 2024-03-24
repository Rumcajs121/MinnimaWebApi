namespace MinimalWebApiLearn.Models;

public class Assignment
{
    public int Id { get; set; }
    public required string Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime EndDate {get; set;}
}
