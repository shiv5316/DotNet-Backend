using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OneToMany.Models
{
    public class Department
    {

        [Key]
        public int DeptId { get; set; }

        public string DeptName { get; set; }

        // One Department → Many Students
        public ICollection<Student> Students { get; set; }
    }
}
