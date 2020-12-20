using ClassLibrary.Entities;

namespace ClassLibrary.Services
{
    public interface ISemesterServices
    {
        void AddSemester(string id, Semester semester);
    }
}