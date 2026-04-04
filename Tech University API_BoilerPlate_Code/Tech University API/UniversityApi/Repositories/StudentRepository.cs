using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UniversityApi.Data;
using UniversityApi.Interfaces;
using UniversityApi.Models;

namespace UniversityApi.Repositories
{
    public class StudentRepository : IStudent
    {
        private readonly UniversityContext _context;

        public StudentRepository(UniversityContext context)
        {
            _context = context;
        }

        public bool DeleteStudent(int studentId)
        {
            var student = _context.Students.Include(s => s.Enrollments)
                                           .FirstOrDefault(s => s.StudentId == studentId);
            if (student == null)
                return false;

            _context.Students.Remove(student);
            return _context.SaveChanges() > 0;
        }

        public IEnumerable<Student> GetStudentsByCourseTitle(string courseTitle)
        {
            if (string.IsNullOrWhiteSpace(courseTitle))
                return Enumerable.Empty<Student>();

            var students = _context.Enrollments
                                   .Include(e => e.Student)
                                   .Include(e => e.Course)
                                   .Where(e => e.Course.Title == courseTitle)
                                   .Select(e => e.Student)
                                   .Distinct()
                                   .ToList();

            return students;
        }
    }
}
