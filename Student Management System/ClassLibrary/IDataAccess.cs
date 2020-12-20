using System.Collections.Generic;
using System.Text;
using ClassLibrary.Entities;

namespace ClassLibrary
{
    public interface IDataAccess
    {
        void SaveData(List<Student> data);
        void SaveData(string id, Semester data);
        void LoadData(string id);
        void DeleteData(string id);
        void LoadAllData();
    }
}
