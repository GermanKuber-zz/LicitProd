using LicitProd.Entities;
using LicitProd.Infraestructure;
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
            if (mainAsm == null)
                mainAsm = Assembly.GetCallingAssembly();
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
            var objectToDbMapper = (IObjectToDbMapper<TEntity>)Activator.CreateInstance(typeToCreate);
            AddToCache(objectToDbMapper);
            return objectToDbMapper;
        }
        public static void AddToCache(IObjectToDbMapper<TEntity> objectToDbMapper)
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
}
