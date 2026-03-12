using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneToMany.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        public string StudentName { get; set; }

        public int DeptId { get; set; }

        [ForeignKey("DeptId")]
        public Department Department { get; set; }
    }
}
