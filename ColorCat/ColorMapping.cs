using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ColorCat
{
    public class Color
    {
        [JsonIgnore]
        public ConsoleColor ConsoleColor { get; set; }

        public string Name
        {
            get { return ConsoleColor.ToString(); }
            set { ConsoleColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), value); }
        }

        public static implicit operator Color(ConsoleColor consoleColor)
        {
            return new Color { ConsoleColor = consoleColor };
        }

        public static implicit operator ConsoleColor(Color color)
        {
            return color.ConsoleColor;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Color);
        }

        public bool Equals(Color that)
        {
            if (that == null) return false;

            return this.ConsoleColor == that.ConsoleColor;
        }

        public override int GetHashCode()
        {
            return ConsoleColor.GetHashCode();
        }

        public override string ToString()
        {
            return ConsoleColor.ToString();
        }
    }

    public class ColorMapping
    {
        public Color Color { get; set; }
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