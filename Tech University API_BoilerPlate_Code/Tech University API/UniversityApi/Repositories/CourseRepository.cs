using Microsoft.EntityFrameworkCore;
using UniversityApi.Data;
using UniversityApi.Interfaces;
using UniversityApi.Models;

namespace UniversityApi.Repositories
{
    public class CourseRepository : ICourse
    {
        private readonly UniversityContext _context;

        public CourseRepository(UniversityContext context)
        {
            _context = context;
        }

        public bool UpdateCourse(Course course)
        {
            var existing = _context.Courses.FirstOrDefault(c => c.CourseId == course.CourseId);
            if (existing == null)
                return false;

            existing.Title = course.Title;
            // Note: updating navigations (Enrollments / InstructorCourses) should be handled separately

            _context.Courses.Update(existing);
            return _context.SaveChanges() > 0;
        }

        public IEnumerable<Course> GetCoursesWithEnrollmentsAboveGrade(int grade)
        {
            var courses = _context.Enrollments
                                  .Include(e => e.Course)
                                  .Where(e => e.Grade > grade)
                                  .Select(e => e.Course)
                                  .Distinct()
                                  .ToList();

            return courses;
        }

        public IEnumerable<Course> GetCoursesByInstructorName(string instructorName)
        {
            if (string.IsNullOrWhiteSpace(instructorName))
                return Enumerable.Empty<Course>();

            var courses = _context.InstructorCourses
                                  .Include(ic => ic.Course)
                                  .Include(ic => ic.Instructor)
                                  .Where(ic => ic.Instructor.Name.Contains(instructorName))
                                  .Select(ic => ic.Course)
                                  .Distinct()
                                  .ToList();

            return courses;
        }
    }
}
