
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
            semester.Year = Year;
            semester.SemesterCode = SemesterCode;
            _semesterServices.AddSemester(id, semester);
        }
    }
}
