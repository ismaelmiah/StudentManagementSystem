using System;
using System.Threading.Channels;
using Autofac;
using ClassLibrary;
using ClassLibrary.Entities;
using ClassLibrary.Models;
using ClassLibrary.Utility;

namespace Student_Management_System
{
    public class Application : IApplication
    {
        private readonly IDataAccess _dataAccess;

        public Application(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public void Run()
        {
            while (true)
            {
                Console.WriteLine("\n\n\tStudent Management System\n\n\tList Of Students\n-----");
                ListOfStudents();
                Console.WriteLine("1. Add New Student");
                Console.WriteLine("2. View Student Details");
                Console.WriteLine("3. Delete Student");
                var input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    MainMenu((Convert.ToInt32(input)));
                }
            }
        }

        private void ListOfStudents()
        {
            _dataAccess.LoadAllData();
        }
        private void MainMenu(int main)
        {
            switch (main)
            {
                case 1:
                    AddStudentGenerate();
                    break;
                case 2:
                    ViewStudentDetails();
                    break;
                case 3:
                    DeleteStudent();
                    break;
                default:
                    Console.WriteLine("Application Closed\n");
                    Environment.Exit(0);
                    break;
            }
        }

        private void DeleteStudent()
        {
            Console.Write("Enter Student ID to Delete the record: ");
            var id = Console.ReadLine();
            _dataAccess.DeleteData(id);
        }

        private static void ViewStudentDetails()
        {
            var student = ConfigureClass.Configure().Resolve<StudentModel>();
            Console.WriteLine("Enter Student ID to see details: ");
            var id = Console.ReadLine();
            student.ViewStudentDetails(id);
        }

        private static void AddStudentGenerate()
        {
            var student = ConfigureClass.Configure().Resolve<StudentModel>();
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
            Console.Write("\n\tDepartment List\n");
            foreach (var dept in departments)
            {
                Console.WriteLine($"{dept}");
            }

            Console.Write("\nDepartment: ");
            var department = Console.ReadLine();
            var checkDepartment = (Department)Enum.Parse(typeof(Department), department ?? string.Empty);
            student.Department = checkDepartment switch
            {
                Department.ComputerScience => Department.ComputerScience,
                Department.BBA => Department.BBA,
                Department.English => Department.English,
                _ => throw new ArgumentOutOfRangeException()
            };

            var degrees = EnumUtil.GetValues<Degree>();
            Console.Write("\n\tDegree List\n");
            foreach (var deg in degrees)
            {
                Console.WriteLine($"{deg}");
            }

            Console.Write("\nDegree: ");
            var degree = Console.ReadLine();
            var checkDegree = (Degree)Enum.Parse(typeof(Degree), degree ?? string.Empty);
            student.Degree = checkDegree switch
            {
                Degree.BSC => Degree.BSC,
                Degree.BBA => Degree.BBA,
                Degree.BA => Degree.BA,
                Degree.MSC => Degree.MSC,
                Degree.MBA => Degree.MBA,
                Degree.MA => Degree.MA,
                _ => throw new ArgumentOutOfRangeException()
            };
            student.AddStudent(student);

        }
    }
}