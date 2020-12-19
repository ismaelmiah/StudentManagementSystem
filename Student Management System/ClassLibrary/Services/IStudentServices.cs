using ClassLibrary.Entities;

namespace ClassLibrary.Services
{
    public interface IStudentServices
    {
        void AddStudent(Student student);
        void DeleteStudent(Student student);
    }
}