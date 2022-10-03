using AutoMapper;
using System.Reflection;

namespace Applicaton.Common.Mappings
{
    public class AssemblyMappingProfile: Profile
    {
        public AssemblyMappingProfile(Assembly assembly)
        {ApplyMappingFromAssembly(assembly);
                
        }
        private void ApplyMappingFromAssembly(Assembly assembly)
        {
            var allTypes = assembly.GetTypes()
                .Where(t=>t.GetInterfaces()
                .Any(i=>i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IMapWith<>)))
                .ToList();
            foreach (var type in allTypes)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod("Mapping");
                methodInfo?.Invoke(instance, new object[] { this}); 
            }
        }
    }
}
