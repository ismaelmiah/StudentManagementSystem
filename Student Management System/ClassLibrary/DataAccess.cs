using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
        public async void SaveStudent(List<Student> data)
        {
            var prevData = await JsonDeserialization();
            try
            {
                var count = prevData.Where(student => student.StudentId == data.ToArray()[0].StudentId).ToList().Count();
                if (count>=1) throw new InvalidDataException();
                data.AddRange(prevData);
                JsonSerialization(data);
                Console.WriteLine("New Student Added Successfully");
            }
            catch (Exception)
            {
                Console.WriteLine("\n\nStudent not Added.\nThis Student ID is Already Assigned" +
                                  " To Another Student.\n Enter Unique ID");
            }
        }
        #endregion

        #region Add Semester
        public async void SaveSemester(string id, List<Semester> data)
        {

            var students = await JsonDeserialization();
            var reqStudent = students.FirstOrDefault(x => x.StudentId == id);
            var data4 = reqStudent?.SemesterAttend.SelectMany(x => x.Courses.ToList()).ToList();
            var notTakenCourses = new CourseModel().Courses
                .Except(data4 ?? new List<Course>(),
                    new CourseComparer()).ToList();
            
            Console.Write("\n\tCourse List hasn’t taken by this Student.\n");
            foreach (var course1 in notTakenCourses)
            {
                Console.WriteLine(
                    $"{course1.CourseId} - {course1.CourseName} - {course1.InstructorName} - {course1.NumberOfCredits}");
            }
            var without = students.FindAll(x => x.StudentId != id);
            var newSemester = new Semester { Courses = new List<Course>() };
            while (true)
            {
                Console.Write("\n1. Course Add Done \n2. Add New Course\nInput: ");
                var courseId = Console.ReadLine();
                if (Convert.ToInt32(courseId) == 1) break;
                if(Convert.ToInt32(courseId) !=2) Console.WriteLine("Enter Correct Response.");
                while (true)
                {
                    try
                    {
                        Console.Write("Course Code (XXX YYY) eg: CSC 301: ");
                        var course = Console.ReadLine();
                        var extraCourse = new CourseModel().Courses.FirstOrDefault(x => x.CourseId == course);
                        if(extraCourse==null)
                            throw new InvalidDataException();
                        newSemester.Courses.Add(extraCourse);
                        break;
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine($"Please Enter Right Course Code as it is.\n{exception}\n");
                    }
                }
            }
            newSemester.Year = data[0].Year;
            newSemester.SemesterCode = data[0].SemesterCode;
            if(newSemester.Courses.Any()) reqStudent?.SemesterAttend?.Add(newSemester);
            without.Add(reqStudent);
            var savedData = new List<Student>();
            savedData.AddRange(without);
            JsonSerialization(savedData);
            Console.WriteLine("New Semester Added Successfully");
        }


        #endregion

        private async Task<Tuple<List<Student>, Student>> IsValidStudent(string id)
        {
            var students = await JsonDeserialization();
            var student = students.FirstOrDefault(x => x.StudentId == id);
            return new Tuple<List<Student>, Student>(students, student);
        }
        private async Task<List<Student>> IsValidStudent()
        {
            var students = await JsonDeserialization();
            return students;
        }
        
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

        private async Task<List<Student>> JsonDeserialization()
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
        public async void LoadData(string id)
        {
            var data = await IsValidStudent(id);
            var student = data.Item2;
            if (student == null)
            {
                Console.WriteLine("Student Not Found, Enter Valid Student Id.\nGoing To Main Menu");
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
                                  $"\nDegree: {student.Degree}");
                if (student.SemesterAttend != null)
                {
                    if (!student.SemesterAttend.Any())
                    {
                        Console.WriteLine("Semester: Not Added");
                    }
                    foreach (var semester in student.SemesterAttend)
                    {
                        Console.WriteLine(
                            $"\nSemester: {"".SemCodeResult(semester.SemesterCode)} {semester.Year}");
                        Console.WriteLine($"Courses:");
                        foreach (var semesterCourse in semester.Courses)
                        {
                            Console.WriteLine($"{semesterCourse.CourseId}" +
                                              $" - {semesterCourse.CourseName}" +
                                              $" - {semesterCourse.InstructorName}" +
                                              $" - {semesterCourse.NumberOfCredits}");
                        }
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
        internal static void AddNewSemester(string id)
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
            var student = IsValidStudent(id).Result.Item2;
            var students = IsValidStudent(id).Result.Item1;
            if (student != null)
            {
                var otherStudents = students.FindAll(x => x.StudentId != id);
                JsonSerialization(otherStudents);
                Console.WriteLine("Record Deleted Successful");
            }
            else
            {
                Console.WriteLine("Student Not Found, Enter Valid Student Id.\nGoing To Main Menu");
            }
        }
        #endregion

        #region ListOfStudents

        public async void LoadAllData()
        {
            var students = await IsValidStudent();
            if (students.Count()!=0)
            {
                foreach (var student in students)
                {
                    Console.WriteLine($"\nName: {student.FirstName}\tStudent ID: {student.StudentId}");
                }
                Console.WriteLine("\n");
            }
            else
            {
                Console.WriteLine("*********No Student Added*********\n\n");
            }
        }

        #endregion
    }
}