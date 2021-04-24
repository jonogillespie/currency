using System.Linq;
using ConventionTests.Utils;
using MediatR;
using Newtonsoft.Json;
using Xunit;

namespace ConventionTests.Tests.Application
{
    public class RequestConventionTests
    {
        [Fact]
        public void Command_NameMustEndWith_Command()
        {
            var typeNames = ReflectionUtils
                .GetAssignableTypesFromApplicationLayer(typeof(IRequest))
                .Where(x => !x.Name.EndsWith("Command")).Select(x => x.Name);
            
            Assert.True(!typeNames.Any(),  
                "All types implementing IRequest must have a request name ending with Command." +
                "The following types violate this convention: "+ 
                $"{JsonConvert.SerializeObject(typeNames)}");
        }
        
        [Fact]
        public void Command_NameMustStartWith_AddUpdateDelete()
        {
            var typeNames = ReflectionUtils
                .GetAssignableTypesFromApplicationLayer(typeof(IRequest))
                .Where(x => !x.Name.StartsWith("Add") 
                            && !x.Name.StartsWith("Update") 
                            && !x.Name.StartsWith("Delete"))
                .Select(x => x.Name);
            
            Assert.True(!typeNames.Any(),  
                "All types implementing IRequest must have a request starting with Add, Update, or Delete." +
                "The following types violate this convention: "+ 
                $"{JsonConvert.SerializeObject(typeNames)}");
        }
        
        [Fact]
        public void Query_NameMustEndWith_Query()
        {
            var typeNames = ReflectionUtils
                .GetAssignableTypesFromApplicationLayer(typeof(IRequest<object>))
                .Where(x => !x.Name.EndsWith("Query"))
                .Select(x => x.Name);
            
            Assert.True(!typeNames.Any(),  
                "All types implementing IRequest<T> must have a request name ending with Query." +
                "The following types violate this convention: "+ 
                $"{JsonConvert.SerializeObject(typeNames)}");
        }
        
        [Fact]
        public void Query_NameMustStartWith_Get()
        {
            var typeNames = ReflectionUtils
                .GetAssignableTypesFromApplicationLayer(typeof(IRequest<object>))
                .Where(x => !x.Name.StartsWith("Get")).Select(x => x.Name);
            
            Assert.True(!typeNames.Any(),  
                "All types implementing IRequest<T> must have a request name starting with Get." +
                "The following types violate this convention: "+ 
                $"{JsonConvert.SerializeObject(typeNames)}");
        }
    }
}