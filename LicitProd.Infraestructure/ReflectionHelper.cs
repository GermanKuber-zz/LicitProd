using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LicitProd.Infraestructure
{
    public static class AttributeExtensions
    {
        public static TValue GetAttributeValue<TAttribute, TValue>(
            this Type type,
            Func<TAttribute, TValue> valueSelector)
            where TAttribute : Attribute
        {
            var att = type.GetCustomAttributes(
                typeof(TAttribute), true
            ).FirstOrDefault() as TAttribute;
            if (att != null)
            {
                return valueSelector(att);
            }
            return default(TValue);
        }
    }
    public static class ReflectionHelper
    {
        public static Tuple<bool, IList<Type>> GetClassesImplementingAnInterface(Assembly assemblyToScan, Type implementedInterface)
        {
            if (assemblyToScan == null)
                return Tuple.Create(false, (IList<Type>)null);

            //if (implementedInterface == null || !implementedInterface.IsInterface)
            //    return Tuple.Create(false, (IList<Type>)null);

            IEnumerable<Type> typesInTheAssembly;

            try
            {
                typesInTheAssembly = assemblyToScan.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                typesInTheAssembly = e.Types.Where(t => t != null);
            }

            IList<Type> classesImplementingInterface = new List<Type>();

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
            return Tuple.Create(true, classesImplementingInterface);
        }
    }
}
