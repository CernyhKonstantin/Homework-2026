# HW 23.09.2026 - ASP.NET Core MVC FluentValidation

This project implements the required FluentValidation tasks in English.

## Implemented tasks

1. Added two new custom validators in the Application layer:
   - `CreateStudentValidator`
   - `UpdateStudentValidator`
2. Registered validators with `AddValidatorsFromAssemblyContaining<CreateStudentValidator>()`.
3. Injected `IValidator<T>` into `StudentController`.
4. Used `ValidateAsync()` before create and update operations.
5. Displayed validation errors in the MVC interface.
6. Added a student form that can be used to verify valid and invalid input.

## Validation rules

### CreateStudentValidator
- Name: required, 2-50 characters
- Surname: required, 2-50 characters
- Age: 18-99
- GPA: 0-100

### UpdateStudentValidator
- Name: required, maximum 50 characters
- Surname: required, maximum 50 characters
- Age: 18-99
- GPA: 0-100

## Run

```bash
dotnet restore
dotnet run --project StudentsMVC/StudentsMVC.csproj
```

The project name is **HW 23.09.2026**.
