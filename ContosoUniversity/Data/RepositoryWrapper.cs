namespace ContosoUniversity.Data
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly AppDbContext _appDbContext;
        private IStudentRepository _student;
        private ICourseRepository _course;

        public RepositoryWrapper(AppDbContext appDbContext) {
            _appDbContext = appDbContext;
        }

        public IStudentRepository Student
        {
            get
            {
                if (_student == null)
                {
                    _student = new StudentRepository(_appDbContext);
                }

                return _student;
            }
        }

        public ICourseRepository Course
        {
            get
            {
                if (_course== null)
                {
                    _course = new CourseRepository(_appDbContext);
                }

                return _course;
            }
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }
    }
}
