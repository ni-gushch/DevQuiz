using System.Text.RegularExpressions;

namespace DevQuiz.Libraries.Core.Extensions
{
    /// <summary>
    /// Extensions for string objects
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Extension method for transforming string to SnakeCase format
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToSnakeCase(this string input)
        {
            if (string.IsNullOrEmpty(input)) { return input; }
            var startUnderscores = Regex.Match(input, @"^+");
            var result = startUnderscores + Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
            return result;
        }
    }
}