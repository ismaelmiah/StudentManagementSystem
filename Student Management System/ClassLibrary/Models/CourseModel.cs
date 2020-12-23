using System.Collections.Generic;
using ClassLibrary.Entities;

namespace ClassLibrary.Models
{
    public class CourseModel
    {
        public List<Course> Courses =>
            new List<Course>()
            {
                new Course(){CourseId = "CSC 101",CourseName = "Computer Fundamental", InstructorName = "Tanzib Rahman", NumberOfCredits = 3},
                new Course(){CourseId = "CSC 201",CourseName = "Database Fundamental", InstructorName = "Kawsar Ahamed", NumberOfCredits = 4},
                new Course(){CourseId = "CSC 211",CourseName = "Theory Of Computing", InstructorName = "Marzia Hossain", NumberOfCredits = 4},
                new Course(){CourseId = "CSC 301",CourseName = "System Analysis & Design", InstructorName = "Robel Khan", NumberOfCredits = 4},
                new Course(){CourseId = "CSC 301",CourseName = "Cyber Security", InstructorName = "Rezuwan Islam", NumberOfCredits = 3},
                new Course(){CourseId = "CSC 302",CourseName = "Computer Architecture", InstructorName = "Nafiz Ahamed", NumberOfCredits = 4}
            };
    }
}