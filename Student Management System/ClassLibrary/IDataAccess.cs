using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public interface IDataAccess
    {
        void SaveData<T>(List<T> data);
        void LoadData(string id);
        void LoadAllData();
    }
}
