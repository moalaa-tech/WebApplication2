using System.Reflection;

namespace CRM.WebApp.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            var assembly = typeof(ServiceCollectionExtensions).Assembly;

            var allTypes = assembly.GetTypes();

            var serviceTypes = allTypes
                .Where(t => t.Name.EndsWith("Service") && t.IsClass && !t.IsAbstract);

            foreach (var impl in serviceTypes)
            {
                var interfaceType = impl.GetInterfaces().FirstOrDefault(i => i.Name == "I" + impl.Name);
                if (interfaceType != null)
                {
                    services.AddScoped(interfaceType, impl);
                }
            }
        }
    }
}
