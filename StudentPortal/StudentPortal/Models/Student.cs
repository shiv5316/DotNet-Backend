using System.ComponentModel.DataAnnotations;

namespace StudentPortal.Models
{
    public class Student
    {
        public int Id { get; set; }   // Primary Key

        [Required]
        public string Name { get; set; }

        public int Age { get; set; }

        public string City { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
