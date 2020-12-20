using System.Linq;
using System.Reflection;
using Autofac;
using ClassLibrary;
using ClassLibrary.Models;
using ClassLibrary.Services;

namespace Student_Management_System
{
    public static class ConfigureClass
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Application>().As<IApplication>();
            builder.RegisterType<StudentServices>().As<IStudentServices>();
            builder.RegisterType<DataAccess>().As<IDataAccess>();
            builder.RegisterType<StudentModel>().AsSelf();
            
            
            //builder.RegisterAssemblyTypes(Assembly.Load(nameof(ClassLibrary)))
            //    .Where(x => x.Namespace.Contains("Services"))
            //    .As(a => a.GetInterfaces().FirstOrDefault(i => i.Name == "I" + a.Name));

            
            return builder.Build();
        }
    }
}
