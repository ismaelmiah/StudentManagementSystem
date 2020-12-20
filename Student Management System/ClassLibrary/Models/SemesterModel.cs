
using System;
using System.Collections.Generic;
using System.Linq;
using ClassLibrary.Entities;
using ClassLibrary.Services;

namespace ClassLibrary.Models
{
    public class SemesterModel
    {
        private readonly ISemesterServices _semesterServices;

        public SemesterModel(ISemesterServices semesterServices)
        {
            _semesterServices = semesterServices;
        }
        public string SemesterCode { get; set; }
        public string Year { get; set; }
        public void AddSemester(string id)
        {
            var semester = new Semester {Courses = new List<Course>()};
            Console.WriteLine("\tTo Add a New Student, Enter the following Information\n");
            Console.Write("Semester Code(eg, XXX - First three UPPERCASE letter of semester): ");
            SemesterCode = Console.ReadLine();
            Console.Write("Year: ");
            Year = Console.ReadLine();
            Console.Write($"Year: {Year}");
            Console.Write("\n\tCourse List, Enter Course Code to Added for This Student.\n");
            foreach (var course in new CourseModel().Courses)
            {
                Console.WriteLine($"{course.CourseId} - {course.CourseName} - {course.InstructorName} - {course.NumberOfCredits}");
            }

            while (true)
            {
                Console.Write("\n1. Course Add Done \n2. Add New Course\nInput: ");
                var courseId = Console.ReadLine();
                if (Convert.ToInt32(courseId) == 1) break;
                else
                {
                    Console.Write("Course Code: ");
                    var course = Console.ReadLine();
                    var extraCourse = new CourseModel().Courses.FirstOrDefault(x => x.CourseId == course);
                    if (extraCourse == null) Console.WriteLine("Please Enter Right Course Code as it is.");
                    semester.Courses.Add(extraCourse);
                }
            }

            semester.Year = Year;
            semester.SemesterCode = SemesterCode;
            _semesterServices.AddSemester(id, semester);
        }
    }
}
