using System;
using ClassLibrary.Services;

namespace Student_Management_System
{
    public class Application : IApplication
    {
        private readonly IStudentServices _studentServices;
        public Application(IStudentServices studentServices)
        {
            _studentServices = studentServices;
        }
        public void Run()
        {
            while (true)
            {
                Console.WriteLine("\n\n\tStudent Management System\n\n\tList Of Students\n-----");
                ListOfStudents();
                Console.WriteLine("1 Add New Student");
                Console.WriteLine("2 View Student Details");
                Console.WriteLine("3 Delete Student");
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) continue;
                try
                {
                    MainMenu((Convert.ToInt32(input)));
                }
                catch
                {
                    Console.WriteLine("Wrong Input, Application Closed");
                    Environment.Exit(0);
                }
            }
        }

        private void ListOfStudents()
        {
            _studentServices.ListOfStudent();
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
                    Console.WriteLine("Wrong Input, Application Closed\n");
                    Environment.Exit(0);
                    break;
            }
        }
        private void DeleteStudent()
        {
            _studentServices.DeleteStudent();
        }
        private void ViewStudentDetails()
        {
            _studentServices.ViewStudentDetails();
        }
        private void AddStudentGenerate()
        {
            _studentServices.AddStudent();
        }
    }
}