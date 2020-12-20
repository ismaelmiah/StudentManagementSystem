using System.Net.Mime;
using Autofac;
using ClassLibrary.Models;
using ClassLibrary.Services;

namespace ClassLibrary
{
    public static class ConfigureLibraryClass
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<SemesterModel>().AsSelf();
            builder.RegisterType<SemesterServices>().As<ISemesterServices>();
            builder.RegisterType<DataAccess>().As<IDataAccess>();


            //builder.RegisterAssemblyTypes(Assembly.Load(nameof(ClassLibrary)))
            //    .Where(x => x.Namespace.Contains("Services"))
            //    .As(a => a.GetInterfaces().FirstOrDefault(i => i.Name == "I" + a.Name));


            return builder.Build();
        }
    }
}
