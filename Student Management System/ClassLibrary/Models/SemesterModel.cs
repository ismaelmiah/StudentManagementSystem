
using System;
using System.Collections.Generic;
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
            var semester = new Semester ();
            Console.WriteLine("\tTo Add a New Semester, Enter the following Information\n");
            Console.Write("Semester Code(SUM for Summer,FAL for Fall,SPR for Spring): ");
            SemesterCode = Console.ReadLine();
            Console.Write("Year: ");
            Year = Console.ReadLine();
            semester.Year = Year;
            semester.SemesterCode = SemesterCode;
            semester.Courses = new List<Course>();
            _semesterServices.AddSemester(id, new List<Semester>(){ semester });
        }
    }
}
