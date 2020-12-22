
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
            var semester = new Semester();
            Console.WriteLine("\tTo Add a New Semester, Enter the following Information\n");
            while (true)
            {
                Console.Write("Semester Code(SUM for Summer,FAL for Fall,SPR for Spring): ");
                SemesterCode = Console.ReadLine();
                if (SemesterCode.Equals("SUM")) break;
                else if (SemesterCode.Equals("FAL")) break;
                else if (SemesterCode.Equals("SPR")) break;
                else Console.WriteLine("Enter Semester Code as It Suggested!");
            }

            while (true)
            {
                Console.Write("Year (YYYY): ");
                Year = Console.ReadLine();
                try
                {
                    if (Convert.ToInt32(Year) >= 1000 && Convert.ToInt32(Year) < 10000)
                    {
                        semester.Year = Year;
                    }
                    else throw new InvalidCastException();
                    break;
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"Enter Year with correct format\n{exception}\n"); 
                }
            }
            semester.SemesterCode = SemesterCode;
            semester.Courses = new List<Course>();
            _semesterServices.AddSemester(id, new List<Semester>(){ semester });
        }
    }
}
