using System;

namespace Student_Management_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("\tStudent Management System\n");
            Console.WriteLine("1. Add New Student");
            Console.WriteLine("2. View Student Details");
            Console.WriteLine("3. Delete Student");
            var input = Convert.ToInt32(Console.ReadLine());
            MainMenu(input);
            Console.ReadKey(true);
        }
        private static void MainMenu(int main)
        {
            switch (main)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(main), main, null);
            }
        }
    }
}
