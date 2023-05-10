using Applicaton.Interfaces;
using AutoMapper;
using System.Reflection;

namespace Applicaton.Common.Mappings
{
    public class AssemblyMappingProfile: Profile
    {
        //private readonly IDataContext _dataContext;
        public AssemblyMappingProfile(Assembly assembly)
        {
            ApplyMappingsAssembly(assembly);
        }
        private void ApplyMappingsAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(type => type.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapWith<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod("Mappping");
               methodInfo?.Invoke(instance, new object[] {this});
            }
        }
        
    }
}
