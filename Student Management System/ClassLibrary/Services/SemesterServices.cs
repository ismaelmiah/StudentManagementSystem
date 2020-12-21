using System.Collections.Generic;
using ClassLibrary.Entities;

namespace ClassLibrary.Services
{
    public class SemesterServices : ISemesterServices
    {
        private readonly IDataAccess _dataAccess;

        public SemesterServices(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public void AddSemester(string id, List<Semester> semester)
        {
            _dataAccess.SaveSemester(id, semester);
        }
    }
}