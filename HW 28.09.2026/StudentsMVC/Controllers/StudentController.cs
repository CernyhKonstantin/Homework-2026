using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsMVC.Application.DTOs;

namespace StudentsMVC;

public class StudentController : Controller
{
    private readonly StudentContext _db;
    private readonly IValidator<CreateStudentDto> _createValidator;
    private readonly IValidator<UpdateStudentDto> _updateValidator;

    public StudentController(
        StudentContext context,
        IValidator<CreateStudentDto> createValidator,
        IValidator<UpdateStudentDto> updateValidator)
    {
        _db = context;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<IActionResult> Index()
    {
        var students = await _db.Students.AsNoTracking().ToListAsync();
        return View(students);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new CreateStudentDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateStudentDto dto)
    {
        var result = await _createValidator.ValidateAsync(dto);

        if (!result.IsValid)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            return View(dto);
        }

        var student = new Student
        {
            Name = dto.Name,
            Surname = dto.Surname,
            Age = dto.Age,
            GPA = dto.GPA
        };

        _db.Students.Add(student);
        await _db.SaveChangesAsync();

        TempData["SuccessMessage"] = "Student was created successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int id, UpdateStudentDto dto)
    {
        var result = await _updateValidator.ValidateAsync(dto);

        if (!result.IsValid)
        {
            return BadRequest(result.Errors.Select(error => new
            {
                error.PropertyName,
                error.ErrorMessage
            }));
        }

        var student = await _db.Students.FindAsync(id);
        if (student is null)
        {
            return NotFound(new { message = "Student was not found." });
        }

        student.Name = dto.Name;
        student.Surname = dto.Surname;
        student.Age = dto.Age;
        student.GPA = dto.GPA;

        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
