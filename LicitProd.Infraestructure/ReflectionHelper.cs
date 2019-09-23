using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LicitProd.Infrastructure
{
    public static class ReflectionHelper
    {
        public static IReadOnlyCollection<PropertyInfo> GetListOfProperties<TType>() =>
                         typeof(TType).GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList().AsReadOnly();

  
        public static IList<Type> GetClassesImplementingAnInterface<TType>()
        {
            var classesImplementingInterface = new List<Type>();
            Type implementedInterface = typeof(TType);
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assemblyToScan in assemblies)
            {

                if (assemblyToScan == null)
                    return new List<Type>();


                IEnumerable<Type> typesInTheAssembly;

                try
                {
                    typesInTheAssembly = assemblyToScan.GetTypes()?.Where(x=> x.FullName.Contains("LicitProd"));
                }
                catch (ReflectionTypeLoadException e)
                {
                    typesInTheAssembly = e.Types.Where(t => t != null);
                }

                // if the interface is a generic interface
                if (implementedInterface.IsGenericType)
                {
                    foreach (var typeInTheAssembly in typesInTheAssembly)
                    {
                        if (typeInTheAssembly.IsClass)
                        {
                            var typeInterfaces = typeInTheAssembly.GetInterfaces();
                            foreach (var typeInterface in typeInterfaces)
                            {
                                if (typeInterface.IsGenericType)
                                {
                                    var typeGenericInterface = typeInterface.GetGenericTypeDefinition();
                                    var implementedGenericInterface = implementedInterface.GetGenericTypeDefinition();

                                    if (typeGenericInterface == implementedGenericInterface)
                                    {
                                        classesImplementingInterface.Add(typeInTheAssembly);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (var typeInTheAssembly in typesInTheAssembly)
                    {
                        if (typeInTheAssembly.IsClass)
                        {
                            // if the interface is a non-generic interface
                            if (implementedInterface.IsAssignableFrom(typeInTheAssembly))
                            {
                                classesImplementingInterface.Add(typeInTheAssembly);
                            }
                        }
                    }
                }
            }
            return classesImplementingInterface;
        }   
    }
}
