using HW_27_02_2026.Context;
using HW_27_02_2026.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var services = new ServiceCollection();

services.AddDbContext<AcademyDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

services.AddScoped<SubjectService>();
services.AddScoped<GradeService>();

var provider = services.BuildServiceProvider();

using var scope = provider.CreateScope();

var subjectService = scope.ServiceProvider.GetRequiredService<SubjectService>();
var gradeService = scope.ServiceProvider.GetRequiredService<GradeService>();

while (true)
{
    Console.WriteLine("\n1 - Add Subject");
    Console.WriteLine("2 - Show Subjects");
    Console.WriteLine("3 - Add Grade");
    Console.WriteLine("4 - Exit");

    var input = Console.ReadLine();

    if (input == "1")
    {
        Console.Write("Name: ");
        var name = Console.ReadLine()!;

        Console.Write("Description: ");
        var description = Console.ReadLine();

        subjectService.AddSubject(name, description);
    }
    else if (input == "2")
    {
        var subjects = subjectService.GetAllSubjects();

        foreach (var s in subjects)
            Console.WriteLine($"{s.Id} - {s.Name}");
    }
    else if (input == "3")
    {
        Console.Write("StudentId: ");
        int studentId = int.Parse(Console.ReadLine()!);

        Console.Write("SubjectId: ");
        int subjectId = int.Parse(Console.ReadLine()!);

        Console.Write("Value (1-12): ");
        int value = int.Parse(Console.ReadLine()!);

        gradeService.AddGrade(studentId, subjectId, value);
    }
    else if (input == "4")
    {
        break;
    }
}