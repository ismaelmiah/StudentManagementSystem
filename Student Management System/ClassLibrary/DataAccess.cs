using System;
using System.IO;
using Newtonsoft.Json;

namespace ClassLibrary
{
    public class DataAccess : IDataAccess
    {
        public void SaveData<T>(T data)
        {
            var jsonResult = JsonConvert.SerializeObject(data);
            const string path = @"../../../StoredData.json";
            if (File.Exists(path))
            {
                File.Delete(path);
                using (var tw = new StreamWriter(path, true))
                {
                    tw.WriteLine(jsonResult);
                    tw.Close();
                }
            }
            else if (!File.Exists(path))
            {
                using (var tw = new StreamWriter(path, true))
                {
                    tw.WriteLine(jsonResult);
                    tw.Close();
                }
            }
        }
    }
}