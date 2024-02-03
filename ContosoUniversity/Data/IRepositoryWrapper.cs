namespace ContosoUniversity.Data
{
    public interface IRepositoryWrapper
    {
        IStudentRepository Student { get; }
        ICourseRepository Course { get; }

        void Save();
    }
}
