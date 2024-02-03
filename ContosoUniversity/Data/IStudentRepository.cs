using ContosoUniversity.Models;

namespace ContosoUniversity.Data
{
    public interface IStudentRepository : IRepositoryBase<Student>
    {
        Student GetStudentWithEnrollmentDetails(int id);
    }
}
