using System;
using System.Collections.Generic;
using System.Linq;

namespace ColorCat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var colors = new List<Tuple<string, ConsoleColor>>()
            {
                new Tuple<string, ConsoleColor>("fatal", ConsoleColor.DarkRed),
                new Tuple<string, ConsoleColor>("error", ConsoleColor.Red),
                new Tuple<string, ConsoleColor>("warn", ConsoleColor.Yellow),
                new Tuple<string, ConsoleColor>("debug", ConsoleColor.DarkGray),
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

                var color = colors.Where(c => input.ToLower().Contains(c.Item1))
                                  .FirstOrDefault();

                if (color != null)
                {
                    Console.ForegroundColor = color.Item2;
                }

                Console.WriteLine(input);

                if (color != null)
                {
                    Console.ResetColor();
                }
            }
        }
    }
}