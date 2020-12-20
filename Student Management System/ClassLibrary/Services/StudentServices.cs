using System.IO;
using ClassLibrary.Entities;
using Newtonsoft.Json;

namespace ClassLibrary.Services
{
    public class StudentServices : IStudentServices
    {
        private readonly IDataAccess _dataAccess;

        public StudentServices(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public void AddStudent(Student student)
        {
            _dataAccess.SaveData(student);
        }

        public void DeleteStudent(Student student)
        {

        }
    }
}