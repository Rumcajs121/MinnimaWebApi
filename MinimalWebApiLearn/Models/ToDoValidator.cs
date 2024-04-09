using FluentValidation;
using MinimalWebApiLearn.Models;

namespace MinimalWebApiLearn;

public class ToDoValidator:AbstractValidator<AssignmentDto>
{
    public bool BeAValidDate(string value){
        return DateTime.TryParseExact(value, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out _);
    }
    public ToDoValidator()
    {
        RuleFor(x=>x.Description).NotEmpty()
                                .MinimumLength(3)
                                .WithMessage("Description is empty You read Task");
        RuleFor(x=>x.EndDate)
        .Must(date=>BeAValidDate(date.ToString("yyyy-MM-dd")))
        .WithMessage("Data is wrong format");
    }
}
