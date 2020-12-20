using System;
using System.Collections.Generic;

namespace ClassLibrary.Entities
{
    public class Student
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string StudentId { get; set; }
        public string JoiningBatch { get; set; } = GenerateBatch();
        public Department Department { get; set; }
        public Degree Degree { get; set; }
        public Semester SemesterAttend { get; set; }

        public static string GenerateBatch()
        {
            var months = DateTime.Today.Month;
            var batch = months switch
            {
                <= 3 => $"Spring",
                <= 7 => $"Summer",
                _ => $"Fall"
            };

            batch += $" {DateTime.Today.Year}";
            return batch;
        }

    }
}