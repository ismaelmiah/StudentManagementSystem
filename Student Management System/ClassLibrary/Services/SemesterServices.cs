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
        public void AddSemester(Semester semester)
        {
            //_dataAccess.SaveData(semester);
        }
    }
}