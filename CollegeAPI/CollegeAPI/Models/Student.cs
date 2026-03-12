public partial class Student
{
    public int StudentId { get; set; }

    public string Name { get; set; }

    public string Course { get; set; }

    public virtual Hostel Hostel { get; set; }
}
