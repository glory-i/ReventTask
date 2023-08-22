namespace ReventTask.Models
{
    public class Teacher : BaseEntity
    {
        public string? Subject { get; set; }
        public double? Salary { get; set; }
       
        public ICollection<Classroom>? Classrooms { get; set; }
        public ICollection<Student>? Students { get; set; }

    }
}
