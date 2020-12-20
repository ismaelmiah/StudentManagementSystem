using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using Autofac;
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

        public void SaveData(string id, Semester data)
        {
            var savedData = new List<Student>();
            using (var r = new StreamReader(Path))
            {
                var json = r.ReadToEnd();
                var students = JsonConvert.DeserializeObject<List<Student>>(json);
                var reqStudent = students.FirstOrDefault(x => x.StudentId == id);
                if (reqStudent != null)
                {
                    reqStudent.SemesterAttend = data;
                    var newAll = students.FindAll(x => x.StudentId != id);
                    newAll.Add(reqStudent);
                    savedData.AddRange(newAll);
                }
                else
                {
                    Console.WriteLine("Problem to Adding Semester.");
                }
            }

            var jsonResult = JsonConvert.SerializeObject(savedData, Formatting.Indented);
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
                if (student != null)
                {
                    Console.WriteLine($"\nFull Name: {student.FirstName}\nMiddle Name: {student.MiddleName}\nLast Name: {student.LastName}\nStudent ID: {student.StudentId}\nJoining Batch: {student.JoiningBatch}\nDepartment: {student.Department}\nDegree: {student.Degree}\nSemester: {student.SemesterAttend.SemCodeResult(student.SemesterAttend.SemesterCode)}\nCourses: \n");
                    foreach (var course in student.SemesterAttend.Courses)
                    {
                        Console.WriteLine($"{course.CourseId} - {course.CourseName} - {course.InstructorName} - {course.NumberOfCredits}");
                    }

                    Console.WriteLine("\n");
                }
                else
                {
                    Console.WriteLine("Student Id not Found, Application Closed");
                }
            }

            Console.WriteLine("\n1. Add New Semester\n2. Go to main menu");
            var response = Convert.ToInt32(Console.ReadLine());
            switch (response)
            {
                case 1:
                    var config = ConfigureLibraryClass.Configure();
                    using (var scope = config.BeginLifetimeScope())
                    {
                        var semester = scope.Resolve<SemesterModel>();
                        semester.AddSemester(id);
                    }
                    break;
                case 2:
                    break;
                default:
                    Console.WriteLine("Wrong Input, Application Closed");
                    Environment.Exit(0);
                    break;
            }
        }

        public void LoadAllData()
        {
            throw new NotImplementedException();
        }
    }
}