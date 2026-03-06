using System.Collections.Generic;

namespace EFCodefirstMVC.Models
{
    public class Hostel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        // Navigation property: one hostel can have many students
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}