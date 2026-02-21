namespace MVC2.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }

        // Calculated field (Age^2)
        public int AgeSquare
        {
            get { return Age * Age; }
        }
    }
}
