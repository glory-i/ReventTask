namespace ReventTask.Models
{
    public class Student: BaseEntity
    {
        public int? TeacherId { get; set; }
        public virtual Teacher? Teacher { get; set; }

        public int? ClassromId { get; set; }
        public virtual Classroom? Classroom { get; set; }

        public string? Address { get; set; }
        public int? Age { get; set; }
        

    }
}
