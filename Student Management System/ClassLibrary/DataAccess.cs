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

        #region New Student
        public void SaveStudent(List<Student> data)
        {
            data.AddRange(JsonDeserialization());
            JsonSerialization(data);
        }
        #endregion

        #region Add Semester
        public void SaveSemester(string id, Semester data)
        {

            var students = JsonDeserialization().ToList();
            var reqStudent = students.FirstOrDefault(x => x.StudentId == id);
            reqStudent.SemesterAttend = data;
            var notTakenCourses = new CourseModel().Courses
                .Except(reqStudent?.SemesterAttend.Courses ?? new List<Course>(),
                    new CourseComparer()).ToList();

            Console.Write("\n\tCourse List hasn’t taken by this Student.\n");
            foreach (var course in notTakenCourses)
            {
                Console.WriteLine(
                    $"{course.CourseId} - {course.CourseName} - {course.InstructorName} - {course.NumberOfCredits}");
            }
            var without = students.FindAll(x => x.StudentId != id);

            while (true)
            {
                Console.Write("\n1. Course Add Done \n2. Add New Course\nInput: ");
                var courseId = Console.ReadLine();
                if (Convert.ToInt32(courseId) == 1) break;
                Console.Write("Course Code: ");
                var course = Console.ReadLine();
                var extraCourse = new CourseModel().Courses.FirstOrDefault(x => x.CourseId == course);
                if (extraCourse == null) Console.WriteLine("Please Enter Right Course Code as it is.");
                reqStudent?.SemesterAttend.Courses?.Add(extraCourse);
            }
            without.Add(reqStudent);
            var savedData = new List<Student>();
            savedData.AddRange(without);
            JsonSerialization(savedData);
        }


        #endregion

        #region JsonOperation
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

        private static IEnumerable<Student> JsonDeserialization()
        {
            List<Student> students;
            using (var r = new StreamReader(Path))
            {
                var json = r.ReadToEnd();
                students = JsonConvert.DeserializeObject<List<Student>>(json);
            }
            return students;
        }
        #endregion

        #region ViewStudentDetails
        public void LoadData(string id)
        {
            var students = JsonDeserialization();
            var student = students.FirstOrDefault(x => x.StudentId == id);
            if (student == null)
            {
                Console.WriteLine("Student Id not Found, Application Closed");
            }
            else
            {
                Console.WriteLine($"" +
                                  $"\nFull Name: {student.FirstName}" +
                                  $"\nMiddle Name: {student.MiddleName}" +
                                  $"\nLast Name: {student.LastName}" +
                                  $"\nStudent ID: {student.StudentId}" +
                                  $"\nJoining Batch: {student.JoiningBatch}" +
                                  $"\nDepartment: {student.Department}" +
                                  $"\nDegree: {student.Degree}" +
                                  $"\nSemester: {student.SemesterAttend.SemCodeResult(student.SemesterAttend.SemesterCode)}" +
                                  $"\nCourses: \n");

                if (student.SemesterAttend.Courses != null)
                {
                    foreach (var course in student.SemesterAttend.Courses)
                    {
                        Console.WriteLine($"{course.CourseId}" +
                                          $" - {course.CourseName}" +
                                          $" - {course.InstructorName}" +
                                          $" - {course.NumberOfCredits}");
                    }
                    Console.WriteLine("\n");
                }

                Console.WriteLine("\n1. Add New Semester\n2. Go to main menu");
                var response = Convert.ToInt32(Console.ReadLine());
                switch (response)
                {
                    case 1:
                        AddNewSemester(id);
                        break;
                    case 2:
                        return;
                    default:
                        Console.WriteLine("Wrong Input, Application Closed");
                        Environment.Exit(0);
                        break;
                }

            }
        }
        private static void AddNewSemester(string id)
        {
            var config = ConfigureLibraryClass.Configure();
            using (var scope = config.BeginLifetimeScope())
            {
                var semester = scope.Resolve<SemesterModel>();
                semester.AddSemester(id);
            }
        }

        #endregion

        #region DeleteStudent
        public void DeleteStudent(string id)
        {
            var students = JsonDeserialization().ToList();
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
        #endregion

        #region ListOfStudents


        public void LoadAllData()
        {
            var students = JsonDeserialization();
            if (students != null)
            {
                foreach (var student in students)
                {
                    Console.WriteLine($"\nName: {student.FirstName}" +
                                      $"\tStudent ID: {student.StudentId}");

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

        #endregion
    }
}