using System.Collections.Generic;

namespace ClassLibrary.Entities
{
    public class Semester
    {
        public string SemesterCode { get; set; }
        public string Year { get; set; }
        public List<Course> Courses { get; set; }
    }
}