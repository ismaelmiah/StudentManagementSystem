using System.Collections.Generic;
using ClassLibrary.Entities;

namespace ClassLibrary
{
    public interface IDataAccess
    {
        void SaveStudent(List<Student> data);
        void SaveSemester(string id, List<Semester> data);
        void LoadData(string id);
        //void DeleteStudent(string id);
        //void LoadAllData();
    }
}
