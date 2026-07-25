using HW_27_02_2026.Models;

namespace HW_27_02_2026.Models
{
    public class Subject
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public ICollection<Grade> Grades { get; set; } = new List<Grade>();
    }
}