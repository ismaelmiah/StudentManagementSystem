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
        public string JoiningBatch { get; set; }
        public Department Department { get; set; }
        public Degree Degree { get; set; }
        public Semester SemesterAttend { get; set; }
        //First Name 
        //● Middle Name 
        //● Last Name 
        //● Student ID 
        //● Joining Batch - pre-populated based on the current date.
        //● Department
        //● Degree
    }
}