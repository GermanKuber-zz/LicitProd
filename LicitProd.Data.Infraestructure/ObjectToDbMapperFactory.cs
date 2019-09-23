using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssemblyScanner;
using LicitProd.Data.Infrastructure.Objects;
using LicitProd.Entities;

namespace LicitProd.Data.Infrastructure
{
    public static class ObjectToDbMapperFactory<TEntity> where TEntity : IEntityToDb, new()
    {
        private static Dictionary<Type, IObjectToDbMapper<TEntity>> _cache = new Dictionary<Type, IObjectToDbMapper<TEntity>>();
      
        public static IObjectToDbMapper<TEntity> Create()
        {
            if (IsInCache())
                return GetFromCache();

                var typeToCreate = AssemblyScanner.FromAssemblyInDirectory(new AssemblyFilter(""))
                                        .IncludeNonPublicTypes()
                                        .BasedOn<IObjectToDbMapper<TEntity>>()
                                        .Filter()
                                        .Classes()
                                        .Scan()
                                        .FirstOrDefault(); 



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
