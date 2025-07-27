using System.Reflection;

namespace Renova.Service.Config
{
    public static class ServiceConfiguration
    {
        public static Assembly GetAssembly() => typeof(ServiceConfiguration).Assembly;
    }
}
