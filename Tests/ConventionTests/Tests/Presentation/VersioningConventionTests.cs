using System.Linq;
using ConventionTests.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Presentation.Controllers.Base;
using Xunit;

namespace ConventionTests.Tests.Presentation
{
    public class VersioningConventionTests
    {
        [Fact]
        public void Controllers_MustSupportVersioning()
        {
            var controllers = 
                ReflectionUtils.GetAssignableTypesFromPresentationLayer(typeof(AppController));

            var controllersWithoutVersioning =
                controllers
                    .Where(x => x.GetCustomAttributes(typeof(RouteAttribute), false)
                        .All(y => !((RouteAttribute) y).Template.StartsWith("v{version:apiVersion}")))
                    .Select(x => x.FullName);
            
            Assert.True(!controllersWithoutVersioning.Any(),
                "Controllers must support versioning. " +
                "The following controllers do not have versioning applied to the route: "+ 
                JsonConvert.SerializeObject(controllersWithoutVersioning));

        }
    }
}