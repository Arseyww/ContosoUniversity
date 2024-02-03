using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Data
{
    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
        public StudentRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {
        }

        public Student GetStudentWithEnrollmentDetails(int id)
        {
            return _appDbContext.Students
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                .FirstOrDefault(s => s.StudentID == id);
        }
    }
}
