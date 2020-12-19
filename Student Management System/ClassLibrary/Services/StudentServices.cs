using System.IO;
using ClassLibrary.Entities;
using Newtonsoft.Json;

namespace ClassLibrary.Services
{
    public class StudentServices : IStudentServices
    {
        public void AddStudent(Student student)
        {
            var jsonResult = JsonConvert.SerializeObject(student);
            const string path = @"../../StoredData.json";
            if (File.Exists(path))
            {
                File.Delete(path);
                using (var tw = new StreamWriter(path, true))
                {
                    tw.WriteLine(jsonResult);
                    tw.Close();
                }
            }
            else if(!File.Exists(path))
            {
                using (var tw = new StreamWriter(path, true))
                {
                    tw.WriteLine(jsonResult);
                    tw.Close();
                }
            }
        }

        public void DeleteStudent(Student student)
        {
            
        }
    }
}
