using Applicaton.Interfaces;
using AutoMapper;
using System.Reflection;

namespace Applicaton.Common.Mappings
{
    public class AssemblyMappingProfile: Profile
    {
        private readonly IRepositoryManager _repositoryManager;
        public AssemblyMappingProfile(Assembly assembly, IRepositoryManager repositoryManager)
        {
            ApplyMappingsAssembly(assembly);
            _repositoryManager = repositoryManager;
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
               methodInfo?.Invoke(instance, new object[] {this, _repositoryManager});
            }
        }
        
    }
}
