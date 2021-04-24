using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Presentation.Middleware.ErrorHandling.Helpers
{
    public static class StringHelpers
    {
        public static string ToCamelCase(this string stringToConvert)
        {
            if (stringToConvert == null)
            {
                return null;
            }

            var pattern = new Regex(@"[A-Z]{2,}(?=[A-Z][a-z]+[0-9]*|\b)|[A-Z]?[a-z]+[0-9]*|[A-Z]|[0-9]+");
            
            return new string(new CultureInfo("en-US", false).TextInfo
                .ToTitleCase(string.Join(" ", pattern.Matches(stringToConvert)).ToLower())
                .Replace(@" ", "")
                .Select((x, i) => i == 0 ? char.ToLower(x) : x)
                .ToArray());
        }
    }
}