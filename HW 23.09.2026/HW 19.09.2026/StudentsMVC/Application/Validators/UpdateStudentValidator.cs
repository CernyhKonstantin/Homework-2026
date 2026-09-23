using FluentValidation;
using StudentsMVC.Application.DTOs;

namespace StudentsMVC.Application.Validators;

public class UpdateStudentValidator : AbstractValidator<UpdateStudentDto>
{
    public UpdateStudentValidator()
    {
        RuleFor(student => student.Name)
            .NotEmpty().WithMessage("Student name is required.")
            .MaximumLength(50).WithMessage("Student name cannot exceed 50 characters.");

        RuleFor(student => student.Surname)
            .NotEmpty().WithMessage("Student surname is required.")
            .MaximumLength(50).WithMessage("Student surname cannot exceed 50 characters.");

        RuleFor(student => student.Age)
            .GreaterThanOrEqualTo(18).WithMessage("Student must be at least 18 years old.")
            .LessThanOrEqualTo(99).WithMessage("Student age cannot exceed 99.");

        RuleFor(student => student.GPA)
            .GreaterThanOrEqualTo(0).WithMessage("GPA cannot be negative.")
            .LessThanOrEqualTo(100).WithMessage("GPA cannot exceed 100.");
    }
}
