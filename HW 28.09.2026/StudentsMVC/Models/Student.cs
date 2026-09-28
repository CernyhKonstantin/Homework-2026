namespace StudentsMVC
{
    public class Student
    {
        // Student identifier
        public int Id { get; set; }
        // Student first name
        public string? Name { get; set; }
        // Student last name
        public string? Surname { get; set; }
        // Student age
        public int Age { get; set; }
        // Grade point average
        public double GPA { get; set; }
    }
}