
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
        public string Year { get; set; } = DateTime.Today.Year.ToString();
        public void AddSemester(string id)
        {
            var semester = new Semester {Courses = new List<Course>()};
            Console.WriteLine("\tTo Add a New Student, Enter the following Information\n");
            Console.Write("Semester Code(eg, XXX - First three lower letter of semester): ");
            semester.SemesterCode = Console.ReadLine();
            semester.Year = Year;
            Console.Write($"Year: {Year}");
            Console.Write("\n\tCourse List, Enter Course ID to Added for This Student.\n");
            foreach (var course in new CourseModel().Courses)
            {
                Console.WriteLine($"{course.CourseId} - {course.CourseName} - {course.InstructorName} - {course.NumberOfCredits}");
            }

            Console.WriteLine("\nCourse Code: ");
            var courseId = Console.ReadLine();
            var extraCourse = new CourseModel().Courses.FirstOrDefault(x => x.CourseId == courseId);
            semester.Courses.Add(extraCourse);
            _semesterServices.AddSemester(id, semester);
        }
    }
}
