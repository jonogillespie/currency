using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Application.Interfaces.External;
using Infrastructure.Services;
using Presentation.Controllers;

namespace ConventionTests.Utils
{
    public static class ReflectionUtils
    {
        public static IEnumerable<Type> GetTypesDeclaredInSolution()
        {
            var assemblies = new[]
            {
                typeof(CurrencyResponse).GetTypeInfo()
                    .Assembly,
                typeof(CurrenciesController).GetTypeInfo()
                    .Assembly,
                typeof(BitcoinPriceService).GetTypeInfo()
                    .Assembly
            };

            var  types = 
                assemblies
                .SelectMany(x => x.GetTypes())
                .Where(x => x.Namespace != null &&
                            (x.Namespace.StartsWith("Application") ||
                             x.Namespace.StartsWith("Presentation") ||
                             x.Namespace.StartsWith("Infrastructure")));

            return types;
        }

        public static IEnumerable<Type> GetAssignableTypesFromApplicationLayer(Type t) =>
            GetAssignableTypes(t,
                typeof(CurrencyResponse).GetTypeInfo()
                    .Assembly);

        public static IEnumerable<Type> GetAssignableTypesFromPresentationLayer(Type t) =>
            GetAssignableTypes(t,
                typeof(CurrenciesController).GetTypeInfo()
                    .Assembly);

        private static IEnumerable<Type> GetAssignableTypes(Type t,
            Assembly assembly)
        {
            if (t.IsInterface)
            {
                return assembly.GetTypes()
                    .Where(x => x.IsAssignableFrom(t));
            }

            return assembly.GetTypes()
                .Where(x => x.BaseType == t);
        }
    }
}