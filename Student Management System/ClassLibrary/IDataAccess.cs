using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public interface IDataAccess
    {
        void SaveData<T>(T data);
    }
}
