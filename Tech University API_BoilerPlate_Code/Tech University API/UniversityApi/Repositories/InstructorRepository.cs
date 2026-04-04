using Microsoft.EntityFrameworkCore;
using UniversityApi.Data;
using UniversityApi.Interfaces;
using UniversityApi.Models;

namespace UniversityApi.Repositories
{
    public class InstructorRepository : IInstructor
    {
        private readonly UniversityContext _context;

        public InstructorRepository(UniversityContext context)
        {
            _context = context;
        }

        public bool AddInstructor(Instructor instructor)
        {
            if (instructor == null)
                return false;

            _context.Instructors.Add(instructor);
            return _context.SaveChanges() > 0;
        }

        public IEnumerable<Instructor> GetInstructorsWithCourseCountAbove(int count)
        {
            var instructors = _context.Instructors
                                      .Include(i => i.InstructorCourses)
                                      .Where(i => i.InstructorCourses.Count() > count)
                                      .ToList();

            return instructors;
        }

        public IEnumerable<Instructor> GetInstructorsWithMostEnrollments()
        {
            var instructorEnrollments = _context.InstructorCourses
                                                .Include(ic => ic.Instructor)
                                                .Include(ic => ic.Course)
                                                .ThenInclude(c => c.Enrollments)
                                                .ToList()
                                                .GroupBy(ic => ic.Instructor)
                                                .Select(g => new { Instructor = g.Key, Enrollments = g.SelectMany(ic => ic.Course.Enrollments).Count() })
                                                .ToList();

            if (!instructorEnrollments.Any())
                return Enumerable.Empty<Instructor>();

            var max = instructorEnrollments.Max(x => x.Enrollments);
            var top = instructorEnrollments.Where(x => x.Enrollments == max).Select(x => x.Instructor).ToList();

            return top;
        }
    }
}
