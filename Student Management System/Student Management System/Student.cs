using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace Student_Management_System
{
    public class Student
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string StudentId { get; set; }
        public DateTime JoiningBatch { get; set; }
        public Department Department { get; set; }
        public Degree Degree { get; set; }
        public Semester SemesterAttend { get; set; }
        public List<Course> Courses { get; set; }
    }
}