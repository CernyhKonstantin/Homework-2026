using HW_27_02_2026.Context;
using HW_27_02_2026.Models;
using Microsoft.EntityFrameworkCore;

namespace HW_27_02_2026.Services
{
    public class GradeService
    {
        private readonly AcademyDbContext _context;

        public GradeService(AcademyDbContext context)
        {
            _context = context;
        }

        public void AddGrade(int studentId, int subjectId, int value)
        {
            if (value < 1 || value > 12)
            {
                Console.WriteLine("Grade must be between 1 and 12.");
                return;
            }

            var studentExists = _context.Students.Any(s => s.Id == studentId);
            var subjectExists = _context.Subjects.Any(s => s.Id == subjectId);

            if (!studentExists || !subjectExists)
            {
                Console.WriteLine("Student or Subject does not exist.");
                return;
            }

            var grade = new Grade
            {
                StudentId = studentId,
                SubjectId = subjectId,
                Value = value
            };

            _context.Grades.Add(grade);
            _context.SaveChanges();
        }

        public List<Grade> GetGradesForStudent(int studentId)
        {
            return _context.Grades
                .Include(g => g.Subject)
                .Where(g => g.StudentId == studentId)
                .ToList();
        }

        public List<Grade> GetGradesForSubject(int subjectId)
        {
            return _context.Grades
                .Include(g => g.Student)
                .Where(g => g.SubjectId == subjectId)
                .ToList();
        }

        public void UpdateGrade(int gradeId, int newValue)
        {
            if (newValue < 1 || newValue > 12)
            {
                Console.WriteLine("Grade must be between 1 and 12.");
                return;
            }

            var grade = _context.Grades.Find(gradeId);

            if (grade == null)
            {
                Console.WriteLine("Grade not found.");
                return;
            }

            grade.Value = newValue;
            _context.SaveChanges();
        }

        public void DeleteGrade(int gradeId)
        {
            var grade = _context.Grades.Find(gradeId);

            if (grade == null)
            {
                Console.WriteLine("Grade not found.");
                return;
            }

            _context.Grades.Remove(grade);
            _context.SaveChanges();
        }
    }
}