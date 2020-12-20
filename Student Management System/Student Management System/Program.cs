using Autofac;

namespace Student_Management_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = ConfigureClass.Configure();
            using var scope = config.BeginLifetimeScope();
            var app = scope.Resolve<IApplication>();
            app.Run();
        }
    }
}
