public class Student
{
    public int Id { get; set; }   // Primary Key
    public string Name { get; set; }
    public string Address { get; set; }
    public int Age { get; set; }
    public string City { get; set; }
    public int? HostelId { get; set; }

    public Hostel Hostel { get; set; }
}