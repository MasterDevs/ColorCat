using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ColorCat
{
    public class ColorMapping
    {
        public ConsoleColor Color { get; set; }
        public bool IgnoreCase { get; set; }
        public bool IsRegex { get; set; }
        public string Pattern { get; set; }

        public bool IsMatch(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }
            else if (IsRegex)
            {
                var options = IgnoreCase ? RegexOptions.IgnoreCase : RegexOptions.None;
                return Regex.IsMatch(text, Pattern, options);
            }
            else
            {
                var options = IgnoreCase ? CompareOptions.IgnoreCase : CompareOptions.None;

                // http://stackoverflow.com/questions/444798/case-insensitive-containsstring/444818#444818
                return CultureInfo.InvariantCulture.CompareInfo.IndexOf(text, Pattern, options) >= 0;
            }
        }
    }
}