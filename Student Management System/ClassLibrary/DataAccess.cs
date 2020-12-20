using System;
using System.Collections.Generic;
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
        public void SaveData(List<Student> data)
        {
            using (var r = new StreamReader(Path))
            {
                var json = r.ReadToEnd();
                var students = JsonConvert.DeserializeObject<List<Student>>(json);
                data.AddRange(students);
            }
            JsonSerialization(data);
        }

        public void SaveData(string id, Semester data)
        {
            var savedData = new List<Student>();
            List<Student> students;
            using (var r = new StreamReader(Path))
            {
                var json = r.ReadToEnd();
                students = JsonConvert.DeserializeObject<List<Student>>(json);
            }
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

            JsonSerialization(savedData);
        }

        private static void JsonSerialization<T>(List<T> data)
        {
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
            List<Student> students;
            using (var r = new StreamReader(Path))
            {
                var json = r.ReadToEnd();
                students = JsonConvert.DeserializeObject<List<Student>>(json);
            }
            var student = students.FirstOrDefault(x => x.StudentId == id);
            if (student != null)
            {
                Console.WriteLine($"\nFull Name: {student.FirstName}\nMiddle Name: {student.MiddleName}\nLast Name: {student.LastName}\nStudent ID: {student.StudentId}\nJoining Batch: {student.JoiningBatch}\nDepartment: {student.Department}\nDegree: {student.Degree}\nSemester: {student.SemesterAttend.SemCodeResult(student.SemesterAttend.SemesterCode)}\nCourses: \n");
                if (student.SemesterAttend.Courses != null)
                {

                    foreach (var course in student.SemesterAttend.Courses)
                    {
                        Console.WriteLine($"{course.CourseId} - {course.CourseName} - {course.InstructorName} - {course.NumberOfCredits}");
                    }
                    Console.WriteLine("\n");
                }
            }
            else
            {
                Console.WriteLine("Student Id not Found, Application Closed");
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

        public void DeleteData(string id)
        {
            List<Student> students;
            using (var r = new StreamReader(Path))
            {
                var json = r.ReadToEnd();
                students = JsonConvert.DeserializeObject<List<Student>>(json);
            }
            var student = students.FirstOrDefault(x => x.StudentId == id);
            if (student != null)
            {
                var otherStudents = students.FindAll(x => x.StudentId != id);
                JsonSerialization(otherStudents);
                Console.WriteLine("Record Deleted Successful");
            }

            else
            {
                Console.WriteLine("Student Id Not Found, Check Id.");
            }
        }

        public void LoadAllData()
        {
            List<Student> students;
            using (var r = new StreamReader(Path))
            {
                var json = r.ReadToEnd();
                students = JsonConvert.DeserializeObject<List<Student>>(json);
                
            }
            if (students != null)
            {
                foreach (var student in students)
                {
                    Console.WriteLine($"\nName: {student.FirstName}\tStudent ID: {student.StudentId}");
                    if (student.SemesterAttend.Courses == null || (student.SemesterAttend.Courses != null && student.SemesterAttend.Courses.Count == 0)) continue;
                    Console.WriteLine("Courses: ");
                    foreach (var course in student.SemesterAttend.Courses)
                    {
                        Console.WriteLine($"{course.CourseId} - {course.CourseName} - {course.InstructorName} - {course.NumberOfCredits}");
                    }
                }
                Console.WriteLine("\n");
            }
            else
            {
                Console.WriteLine("No Student Added");
            }
        }
    }
}