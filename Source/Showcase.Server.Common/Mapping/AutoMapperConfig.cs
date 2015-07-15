namespace Showcase.Server.Common.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using System.Web.Mvc;

    using AutoMapper;

    public class AutoMapperConfig
    {
        public static void RegisterMappings(params Assembly[] assemblies)
        {
            Mapper.Configuration.ConstructServicesUsing(t => DependencyResolver.Current.GetService(t));

            var types = new List<Type>();
            foreach (var assembly in assemblies)
            {
                types.AddRange(assembly.GetExportedTypes());
            }

            LoadStandardMappings(types);

            LoadCustomMappings(types);
        }

        private static void LoadStandardMappings(IEnumerable<Type> types)
        {
            var maps = from t in types
                       from i in t.GetInterfaces()
                       where
                           i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>) && !t.IsAbstract
                           && !t.IsInterface
                       select new { Source = i.GetGenericArguments()[0], Destination = t };

            foreach (var map in maps)
            {
                Mapper.CreateMap(map.Source, map.Destination);
                Mapper.CreateMap(map.Destination, map.Source);
            }
        }

        private static void LoadCustomMappings(IEnumerable<Type> types)
        {
            var maps = from t in types
                       from i in t.GetInterfaces()
                       where typeof(IHaveCustomMappings).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface
                       select (IHaveCustomMappings)Activator.CreateInstance(t);

            foreach (var map in maps)
            {
                map.CreateMappings(Mapper.Configuration);
            }
        }
    }
}
