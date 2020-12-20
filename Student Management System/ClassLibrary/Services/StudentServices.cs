using System.Collections.Generic;
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
            _dataAccess.SaveData(new List<Student>(){ student });
        }

        public void DeleteStudent(Student student)
        {

        }

        public void ViewStudentDetails(string Id)
        {
            _dataAccess.LoadData(Id);
        }
    }
}