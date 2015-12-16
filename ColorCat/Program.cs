using System;
using System.Collections.Generic;
using System.Linq;

namespace ColorCat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var mappings = new List<ColorMapping>()
            {
                new ColorMapping{IsRegex=false, IgnoreCase=true, Pattern="fatal", Color =ConsoleColor.DarkRed},
                new ColorMapping{IsRegex=false, IgnoreCase=true, Pattern="error", Color =ConsoleColor.Red},
                new ColorMapping{IsRegex=false, IgnoreCase=true, Pattern="warn", Color =ConsoleColor.Yellow},
                new ColorMapping{IsRegex=false, IgnoreCase=true, Pattern="debug", Color =ConsoleColor.DarkGray},
            };

            bool run = true;

            Console.CancelKeyPress += (s, e) => run = false;

            while (run)
            {
                var input = Console.In.ReadLine();
                if (input == null)
                {
                    run = false;
                    continue;
                }

                var mapping = mappings.FirstOrDefault(m => m.IsMatch(input));

                if (mapping != null)
                {
                    Console.ForegroundColor = mapping.Color;
                }

                Console.WriteLine(input);

                if (mapping != null)
                {
                    Console.ResetColor();
                }
            }
        }
    }
}