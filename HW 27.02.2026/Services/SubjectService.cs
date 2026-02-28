using HW_27_02_2026.Context;
using HW_27_02_2026.Models;
using Microsoft.EntityFrameworkCore;

namespace HW_27_02_2026.Services
{
    public class SubjectService
    {
        private readonly AcademyDbContext _context;

        public SubjectService(AcademyDbContext context)
        {
            _context = context;
        }

        public void AddSubject(string name, string? description)
        {
            var subject = new Subject
            {
                Name = name,
                Description = description
            };

            _context.Subjects.Add(subject);
            _context.SaveChanges();
        }

        public List<Subject> GetAllSubjects()
        {
            return _context.Subjects.ToList();
        }

        public Subject? GetSubjectById(int id)
        {
            return _context.Subjects.Find(id);
        }

        public void UpdateSubject(int id, string name, string? description)
        {
            var subject = _context.Subjects.Find(id);

            if (subject == null)
            {
                Console.WriteLine("Subject not found.");
                return;
            }

            subject.Name = name;
            subject.Description = description;

            _context.SaveChanges();
        }

        public void DeleteSubject(int id)
        {
            var subject = _context.Subjects
                .Include(s => s.Grades)
                .FirstOrDefault(s => s.Id == id);

            if (subject == null)
            {
                Console.WriteLine("Subject not found.");
                return;
            }

            if (subject.Grades.Any())
            {
                Console.WriteLine("Cannot delete subject. It has grades.");
                return;
            }

            _context.Subjects.Remove(subject);
            _context.SaveChanges();
        }
    }
}