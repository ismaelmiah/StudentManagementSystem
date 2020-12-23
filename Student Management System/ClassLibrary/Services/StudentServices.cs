using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
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

        private static (string fName, string mName, string lName) GetNameTuple()
        {
            Console.Write("First Name: ");
            var f = Console.ReadLine();
            Console.Write("Middle Name: ");
            var m = Console.ReadLine();
            Console.Write("Last Name: ");
            var l = Console.ReadLine();
            return (f, m, l);
        }
        public void AddStudent()
        {
            var student = new Student();
            Console.WriteLine("\tTo Add a New Student, Enter the following Information\n");
            var (fName, mName, lName) = GetNameTuple();
            student.FirstName = fName;
            student.MiddleName = mName;
            student.LastName = lName;
            var isOk = true;
            while (isOk)
            {

                Console.Write("\tEnter Student ID format (XXX-XXX-XXX) only number\nStudent ID: ");
                student.StudentId = Console.ReadLine();
                isOk = Regex.IsMatch(student.StudentId ?? string.Empty, @"^\d{3}-\d{3}-\d{3}$");
                switch (isOk)
                {
                    case false:
                        Console.WriteLine("Student ID is not in format, Please enter Correctly");
                        break;
                    default:
                        isOk = false;
                        break;
                }
            }
            Console.Write($"Joining Batch: {student.JoiningBatch}");

            var departments = EnumUtil.GetValues<Department>();
            Console.Write("\n\n\tDepartment List\n");
            foreach (var dept in departments) Console.WriteLine(dept);
            while (!isOk)
            {
                Console.Write("\nDepartment: ");
                var department = Console.ReadLine();
                try
                {
                    var checkDepartment = (Department)Enum.Parse(typeof(Department), department ?? string.Empty);
                    switch (checkDepartment)
                    {
                        case Department.ComputerScience:
                            student.Department = Department.ComputerScience;
                            isOk = true;
                            break;
                        case Department.BBA:
                            student.Department = Department.BBA;
                            isOk = true;
                            break;
                        case Department.English:
                            isOk = true;
                            student.Department = Department.English;
                            break;
                        default:
                            throw new InvalidEnumArgumentException();
                    }
                }
                catch(Exception exception)
                {
                    Console.WriteLine($"Enter Department Name as it is.\n{exception}\n");
                }
            }

            var degrees = EnumUtil.GetValues<Degree>();
            Console.Write("\n\n\tDegree List\n");
            foreach (var deg in degrees) Console.WriteLine(deg);

            while (isOk)
            {
                Console.Write("\nDegree: ");
                var degree = Console.ReadLine();
                try
                {
                    var checkDegree = (Degree)Enum.Parse(typeof(Degree), degree ?? string.Empty);
                    switch (checkDegree)
                    {
                        case Degree.BSC:
                            student.Degree = Degree.BSC;
                            isOk = false;
                            break;
                        case Degree.BBA:
                            student.Degree = Degree.BBA;
                            isOk = false;
                            break;
                        case Degree.BA:
                            student.Degree = Degree.BA;
                            isOk = false;
                            break;
                        case Degree.MSC:
                            student.Degree = Degree.MSC;
                            isOk = false;
                            break;
                        case Degree.MBA:
                            student.Degree = Degree.MBA;
                            isOk = false;
                            break;
                        case Degree.MA:
                            student.Degree = Degree.MA;
                            isOk = false;
                            break;
                        default:
                            throw new InvalidEnumArgumentException();
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"Enter Degree Name as it is.\n{exception}\n");
                }

            }

            student.SemesterAttend = new List<Semester>();
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