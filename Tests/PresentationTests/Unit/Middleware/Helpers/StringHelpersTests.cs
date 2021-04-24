using Presentation.Middleware.ErrorHandling.Helpers;
using Xunit;

namespace PresentationTests.Unit.Middleware.Helpers
{
    public class StringHelpersTests
    {
        [Fact]
        public void ToCamelCase_StringNull_ReturnsNull()
        {
            string testString = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var camelCase = testString.ToCamelCase();
            
            Assert.Null(camelCase);
        }

        [Theory]
        [InlineData("HelloWorld", "helloWorld")]
        [InlineData("Hello", "hello")]
        [InlineData("HelloWORLD", "helloWorld")]
        [InlineData("HelloWorld1", "helloWorld1")]
        [InlineData("HelloWorldOne", "helloWorldOne")]
        public void ToCamelCase_Various_ReturnsCamelCase(string testString, string expected)
        {
            var camelCase = testString.ToCamelCase();
            
            Assert.Equal(expected, camelCase);
        }
    }
}