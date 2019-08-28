using LicitProd.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LicitProd.Data
{
    public static class ObjectToDbMapperFactory<TEntity> where TEntity : IEntityToDb, new()
    {
        private static Dictionary<Type, IObjectToDbMapper<TEntity>> _cache = new Dictionary<Type, IObjectToDbMapper<TEntity>>();
        private static List<Assembly> GetListOfEntryAssemblyWithReferences()
        {
            List<Assembly> listOfAssemblies = new List<Assembly>();
            var mainAsm = Assembly.GetEntryAssembly();
            listOfAssemblies.Add(mainAsm);

            foreach (var refAsmName in mainAsm.GetReferencedAssemblies())
                listOfAssemblies.Add(Assembly.Load(refAsmName));

            return listOfAssemblies;
        }
        public static IObjectToDbMapper<TEntity> Create()
        {
            if (IsInCache())
                return GetFromCache();

            var types = new List<Type>();
            var assemblies = GetListOfEntryAssemblyWithReferences();
            assemblies.Where(x => x.FullName.Contains("LicitProd"))
                ?.Select(assembly => ReflectionHelper.GetClassesImplementingAnInterface(assembly, typeof(IObjectToDbMapper<TEntity>)).Item2)
                                ?.ToList()
                                .ForEach(x => x.ToList().ForEach(xType => types.Add(xType)));

            var typeToCreate = types.FirstOrDefault(x => x.Name.Contains(typeof(TEntity).Name));

            if (typeToCreate == null)
                throw new Exception("El DbMapper requerido no existe : " + typeToCreate.ToString());
            var objectToDbMapper= (IObjectToDbMapper<TEntity>)Activator.CreateInstance(typeToCreate);
            AddToCache(objectToDbMapper);
            return objectToDbMapper;
        }
        public static void AddToCache( IObjectToDbMapper<TEntity> objectToDbMapper)
        {
            _cache.Add(typeof(TEntity), objectToDbMapper);
        }
        public static bool IsInCache()
        {
            return _cache.Any(x => x.Key == typeof(TEntity));
        }
        public static IObjectToDbMapper<TEntity> GetFromCache()
        {
            return _cache.First(x => x.Key == typeof(TEntity)).Value;
        }
    }
    //TODO: refactorizar y quitar esta clase repetida
    public static class ReflectionHelper
    {
        public static Tuple<bool, IList<Type>> GetClassesImplementingAnInterface(Assembly assemblyToScan, Type implementedInterface)
        {
            if (assemblyToScan == null)
                return Tuple.Create(false, (IList<Type>)null);

            if (implementedInterface == null || !implementedInterface.IsInterface)
                return Tuple.Create(false, (IList<Type>)null);

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
