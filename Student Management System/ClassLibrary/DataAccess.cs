using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClassLibrary.Entities;
using ClassLibrary.Models;
using Newtonsoft.Json;

namespace ClassLibrary
{
    public class DataAccess : IDataAccess
    {
        private const string Path = @"../../../StoredData.json";
        public void SaveData<T>(List<T> data)
        {
            using (var r = new StreamReader(Path))
            {
                var json = r.ReadToEnd();
                var students = JsonConvert.DeserializeObject<List<Student>>(json);
                data.AddRange((IEnumerable<T>)students);
            }
            var jsonResult = JsonConvert.SerializeObject(data, Formatting.Indented);
            if (File.Exists(Path))
            {
                File.Delete(Path);
                using (var tw = new StreamWriter(Path, true))
                {
                    tw.WriteLine(jsonResult);
                    tw.Close();
                }
            }
            else if (!File.Exists(Path))
            {
                using (var tw = new StreamWriter(Path, true))
                {
                    tw.WriteLine(jsonResult);
                    tw.Close();
                }
            }
        }

        public void LoadData(string id)
        {
            using (var r = new StreamReader(Path))
            {
                var json = r.ReadToEnd();
                var students = JsonConvert.DeserializeObject<List<Student>>(json);
                var student = students.FirstOrDefault(x => x.StudentId == id);
                    Console.WriteLine($"\nFull Name: {student.FirstName}\nMiddle Name: {student.MiddleName}\nLast Name: {student.LastName}\nStudent ID: {student.StudentId}\nJoining Batch: {student.JoiningBatch}\nDepartment: {student.Department}\nDegree: {student.Degree}\nSemester: {student.SemesterAttend}\nCourses: {student.Courses}");
            }

        }

        public void LoadAllData()
        {
            throw new NotImplementedException();
        }
    }
}