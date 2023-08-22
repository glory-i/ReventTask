namespace ReventTask.Models
{
    public class Classroom : BaseEntity
    {
        public int? TeacherId { get; set; }
        public virtual Teacher? Teacher { get; set; }

        public ICollection<Student>? Students { get; set; }

        public int? Capacity { get; set; }
        public string? Location { get; set; }
    }
}
