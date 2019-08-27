using LicitProd.Entities;
using LicitProd.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LicitProd.Data
{
    public class BaseRepository<TEntity> where TEntity : IEntityToDb
    {
        protected readonly SqlAccessService SqlAccessService;

        public BaseRepository()
        {
            SqlAccessService = new SqlAccessService();

        }
        public List<TEntity> Get() {
            var dc = new Default();
            var loadableTypes = GetClassesImplementingAnInterface(dc.GetType().Assembly, typeof(DbMapper<>)).Item2;
            var mapper = (DbMapper<TEntity>)Activator.CreateInstance(loadableTypes.First());
            return mapper.MapList(SqlAccessService.SelectData("Logs",
                 EntityToColumns<TEntity>.Map()
                .Send()));

        }
            
   

        public void GetMapper()
        {
            var objects = Assembly.GetExecutingAssembly().GetTypes()
              .Where(x => x.GetInterfaces().Any(y => y.IsGenericType ))
              .Select(x => (DbMapper<IEntityToDb>)Activator.CreateInstance(x, new object[] { 0 })).ToList();
            objects.ForEach(x => Console.WriteLine(x.GetType().Name));
            
        }
        public  Tuple<bool, IList<Type>> GetClassesImplementingAnInterface(Assembly assemblyToScan, Type implementedInterface)
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
