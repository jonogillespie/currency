using System.Linq;
using System.Reflection;
using ConventionTests.Utils;
using Newtonsoft.Json;
using Xunit;

namespace ConventionTests.Tests.Solution
{
    public class MethodConventionTests
    {
        [Fact]
        public void PublicMethodParameters_Count_LessThan4()
        {
            var types =
                ReflectionUtils.GetTypesDeclaredInSolution();
         
            var methods =
                types.SelectMany(x => x.GetMethods(BindingFlags.Public
                                                   | BindingFlags.Instance
                                                   | BindingFlags.DeclaredOnly));

            var methodsWithMoreThan3Parameters =
                methods.Where(x => x.GetParameters().ToList().Count > 3)
                    .Select(x => $"{x.DeclaringType?.FullName}.{x.Name}");
            
            Assert.True(!methodsWithMoreThan3Parameters.Any(),
                "Public methods cannot have more than three parameters." +
                "The following methods violate this convention: "+ 
                JsonConvert.SerializeObject(methodsWithMoreThan3Parameters));
        }
    }
}