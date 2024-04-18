using FluentValidation;
using MinimalWebApiLearn.Models;

namespace MinimalWebApiLearn;

public class ToDoValidator:AbstractValidator<AssignmentDto>
{

    private bool BeValidDate(string date)
    {
        return DateTime.TryParse(date, out _);
    }
    public ToDoValidator()
    {
        RuleFor(x=>x.Description).NotEmpty()
                                .MinimumLength(3)
                                .WithMessage("Description is empty You read Task");
        RuleFor(x=>x.EndDate).NotEmpty().WithMessage("Date is required").Must(BeValidDate).WithMessage("Property dataTime must RRRR-MM-DD format.");
    }
}
