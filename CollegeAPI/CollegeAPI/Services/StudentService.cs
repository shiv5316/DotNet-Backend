using CollegeAPI.Models;
using Microsoft.EntityFrameworkCore;

public class StudentService : IStudentService
{
    private readonly CollegeContext _context;

    public StudentService(CollegeContext context)
    {
        _context = context;
    }

    // Read All Students
    public List<StudentDTO> GetAllStudents()
    {
        var data = _context.Students
            .Include(x => x.Hostel)
            .Select(x => new StudentDTO
            {
                StudentId = x.StudentId,
                Name = x.Name,
                Course = x.Course,
                RoomNo = (int)(x.Hostel != null ? x.Hostel.RoomNo : null)
            }).ToList();

        return data;
    }

    // Students in hostel
    public List<StudentDTO> GetStudentsInHostel()
    {
        var data = _context.Students
            .Include(x => x.Hostel)
            .Where(x => x.Hostel != null)
            .Select(x => new StudentDTO
            {
                StudentId = x.StudentId,
                Name = x.Name,
                Course = x.Course,
                RoomNo = (int)x.Hostel.RoomNo
            }).ToList();

        return data;
    }

    // Update Room
    public void UpdateRoom(int studentId, int roomNo)
    {
        var hostel = _context.Hostels
            .FirstOrDefault(x => x.StudentId == studentId);

        if (hostel != null)
        {
            hostel.RoomNo = roomNo;
            _context.SaveChanges();
        }
    }

    // Delete Student
    public void DeleteStudent(int studentId)
    {
        var student = _context.Students.Find(studentId);

        if (student != null)
        {
            _context.Students.Remove(student);
            _context.SaveChanges();
        }
    }
}