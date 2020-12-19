using System;
using ClassLibrary.Entities;

namespace ClassLibrary.Services
{
    public class Application : IApplication
    {
        public void Run()
        {
            Console.WriteLine("\tStudent Management System\n");
            Console.WriteLine("1. Add New Student");
            Console.WriteLine("2. View Student Details");
            Console.WriteLine("3. Delete Student");
            var input = Convert.ToInt32(Console.ReadLine());
            MainMenu(input);
        }
        private static void MainMenu(int main)
        {
            switch (main)
            {
                case 1:
                    AddStudentGenerate();
                    break;
                case 2:
                    Console.WriteLine("Application Closing, Thank You");
                    break;
                case 3:
                    Console.WriteLine("Application Closing, Thank You");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(main), main, null);
            }
        }
        private static void AddStudentGenerate()
        {
            var student = new StudentModel();
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
            var checkDepartment = (Department) Enum.Parse(typeof(Department), department ?? string.Empty);
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
            var checkDegree = (Degree) Enum.Parse(typeof(Degree), degree ?? string.Empty);
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