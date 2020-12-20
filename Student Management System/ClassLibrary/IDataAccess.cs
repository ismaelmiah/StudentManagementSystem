using System.Collections.Generic;
using System.Text;
using ClassLibrary.Entities;

namespace ClassLibrary
{
    public interface IDataAccess
    {
        void SaveData<T>(List<T> data);
        void SaveData(string id, Semester data);
        void LoadData(string id);
        void LoadAllData();
    }
}
