using System;
using System.Collections.Generic;
using ClassLibrary.Entities;
using ClassLibrary.Utility;

namespace ClassLibrary.Services
{
    public class StudentServices : IStudentServices
    {
        private readonly IDataAccess _dataAccess;
        public StudentServices(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public void AddStudent()
        {
            var student = new Student();
            Console.WriteLine("\tTo Add a New Student, Enter the following Information\n");
            Console.Write("First Name: ");
            student.FirstName = Console.ReadLine();
            Console.Write("Middle Name: ");
            student.MiddleName = Console.ReadLine();
            Console.Write("Last Name: ");
            student.LastName = Console.ReadLine();
            Console.Write("Student ID: ");
            student.StudentId = Console.ReadLine();
            Console.Write($"Joining Batch: {student.JoiningBatch}");

            var departments = EnumUtil.GetValues<Department>();
            Console.Write("\n\n\tDepartment List\n");
            foreach (var dept in departments) Console.WriteLine(dept);

            Console.Write("\nDepartment: ");
            var department = Console.ReadLine();
            try
            {
                var checkDepartment = (Department)Enum.Parse(typeof(Department), department ?? string.Empty);
                switch (checkDepartment)
                {
                    case Department.ComputerScience:
                        student.Department = Department.ComputerScience;
                        break;
                    case Department.BBA:
                        student.Department = Department.BBA;
                        break;
                    case Department.English:
                        student.Department = Department.English;
                        break;
                    default:
                        Console.WriteLine("Wrong Input----");
                        break;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            var degrees = EnumUtil.GetValues<Degree>();
            Console.Write("\n\n\tDegree List\n");
            foreach (var deg in degrees) Console.WriteLine(deg);

            Console.Write("\nDegree: ");
            var degree = Console.ReadLine();
            var checkDegree = (Degree)Enum.Parse(typeof(Degree), degree ?? string.Empty);
            switch (checkDegree)
            {
                case Degree.BSC:
                    student.Degree = Degree.BSC;
                    break;
                case Degree.BBA:
                    student.Degree = Degree.BBA;
                    break;
                case Degree.BA:
                    student.Degree = Degree.BA;
                    break;
                case Degree.MSC:
                    student.Degree = Degree.MSC;
                    break;
                case Degree.MBA:
                    student.Degree = Degree.MBA;
                    break;
                case Degree.MA:
                    student.Degree = Degree.MA;
                    break;
                default:
                    Console.WriteLine("Wrong Input----");
                    break;
            }
            student.SemesterAttend = new Semester();
            _dataAccess.SaveStudent(new List<Student>(){ student });
        }

        public void DeleteStudent()
        {
            Console.Write("Enter Student ID to Delete the record: ");
            var id = Console.ReadLine();
            _dataAccess.DeleteStudent(id);
        }

        public void ViewStudentDetails()
        {
            Console.Write("Enter Student ID to see details: ");
            var id = Console.ReadLine();
            _dataAccess.LoadData(id);
        }

        public void ListOfStudent()
        {
            _dataAccess.LoadAllData();
        }
    }
}