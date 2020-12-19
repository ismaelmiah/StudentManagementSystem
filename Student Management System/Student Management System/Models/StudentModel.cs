using System;
using System.Collections.Generic;
using ClassLibrary.Entities;
using ClassLibrary.Services;

namespace Student_Management_System.Models
{
    public class StudentModel
    {
        private readonly IStudentServices _studentServices;

        public StudentModel(IStudentServices studentServices)
        {
            _studentServices = studentServices;
        }

        public StudentModel()
        {

        }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string StudentId { get; set; }

        public string JoiningBatch { get; set; } = GenerateBatch();

        public Department Department { get; set; }
        public Degree Degree { get; set; }

        public void AddStudent(StudentModel model)
        {
            var student = new Student
            {
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                StudentId = model.StudentId,
                JoiningBatch = model.JoiningBatch,
                Department = model.Department,
                Degree = model.Degree,
                Courses = new List<Course>(),
                SemesterAttend = new Semester()
            };
            _studentServices.AddStudent(student);
        }

        public void DeleteStudent(StudentModel model)
        {
            var student = new Student
            {
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                StudentId = model.StudentId,
                JoiningBatch = model.JoiningBatch,
                Department = model.Department,
                Degree = model.Degree,
                Courses = new List<Course>(),
                SemesterAttend = new Semester()
            };
            _studentServices.AddStudent(student);
        }

        public static string GenerateBatch()
        {
            var months = DateTime.Today.Month;
            var batch = "";
            if (months <= 3)
            {
                batch = $"Spring";
            }
            else if (months <= 7)
            {
                batch = $"Summer";
            }
            else
            {
                batch = $"Fall";
            }

            batch += $" {DateTime.Today.Year}";
            return batch;
        }
    }
}
