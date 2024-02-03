namespace ContosoUniversity.Models.ViewModels
{
    public class StudentListViewModel
    {
        public IEnumerable<Student> Students { get; set; }
        public PagingInfoViewModel PagingInfo { get; set; }
    }
}
