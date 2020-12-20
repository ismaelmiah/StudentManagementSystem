using System;
using ClassLibrary.Entities;
using ClassLibrary.Services;

namespace ClassLibrary.Models
{
    public class StudentModel
    {
        private readonly IStudentServices _studentServices;

        public StudentModel(IStudentServices studentServices)
        {
            _studentServices = studentServices;
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
                SemesterAttend = new Semester()
            };
            _studentServices.AddStudent(student);
        }

        public void ViewStudentDetails(string id)
        {
            _studentServices.ViewStudentDetails(id);
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
