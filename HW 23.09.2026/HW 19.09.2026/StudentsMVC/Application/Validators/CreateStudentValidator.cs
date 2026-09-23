using FluentValidation;
using StudentsMVC.Application.DTOs;

namespace StudentsMVC.Application.Validators;

public class CreateStudentValidator : AbstractValidator<CreateStudentDto>
{
    public CreateStudentValidator()
    {
        RuleFor(student => student.Name)
            .NotEmpty().WithMessage("Student name is required.")
            .Length(2, 50).WithMessage("Student name must contain between 2 and 50 characters.");

        RuleFor(student => student.Surname)
            .NotEmpty().WithMessage("Student surname is required.")
            .Length(2, 50).WithMessage("Student surname must contain between 2 and 50 characters.");

        RuleFor(student => student.Age)
            .InclusiveBetween(18, 99).WithMessage("Student age must be between 18 and 99.");

        RuleFor(student => student.GPA)
            .InclusiveBetween(0, 100).WithMessage("GPA must be between 0 and 100.");
    }
}
