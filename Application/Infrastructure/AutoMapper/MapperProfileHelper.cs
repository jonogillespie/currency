using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Application.Infrastructure.AutoMapper
{
    public static class MapperProfileHelper
    {
        public static IEnumerable<IHaveCustomMapping> LoadCustomMappings(Assembly rootAssembly)
        {
            var types = rootAssembly.GetExportedTypes();

            var mapsFrom = (
                from type in types
                from instance in type.GetInterfaces()
                where
                    typeof(IHaveCustomMapping).IsAssignableFrom(type) &&
                    !type.IsAbstract &&
                    !type.IsInterface
                select (IHaveCustomMapping) Activator.CreateInstance(type)).ToList();

            return mapsFrom;
        }
    }
}