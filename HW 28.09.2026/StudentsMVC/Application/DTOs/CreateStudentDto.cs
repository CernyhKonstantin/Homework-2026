namespace StudentsMVC.Application.DTOs;

public class CreateStudentDto
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public int Age { get; set; }
    public double GPA { get; set; }
}
