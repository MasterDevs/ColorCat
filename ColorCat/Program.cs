﻿using System;
using System.Linq;

namespace ColorCat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Run();
            }
            else
            {
                var options = new ColorCatOptions();

                if (CommandLine.Parser.Default.ParseArguments(args, options, (_, __) => { }))
                {
                    if (options.Add != null)
                    {
                        AddConfig(options.Add);
                    }
                }
            }
        }

        public static void Run()
        {
            var config = Configuration.Load();

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

                var mapping = config.Mappings.FirstOrDefault(m => m.IsMatch(input));

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

        private static void AddConfig(ColorCatOptions.AddOption options)
        {
            var config = Configuration.Load();
            var newMapping = CreateMapping(options);
            config.Mappings.Add(newMapping);

            var mappingJson = config.Save();

            if (options.Verbose)
            {
                Console.WriteLine("Added mapping.  Current mappings are:");
                Console.WriteLine(mappingJson);
            }
        }

        private static ColorMapping CreateMapping(ColorCatOptions.AddOption options)
        {
            var mapping = new ColorMapping
            {
                Color = options.Color,
                IgnoreCase = options.IgnoreCase,
                IsRegex = options.IsRegex,
                Pattern = string.Join(" ", options.Pattern),
            };

            if (string.IsNullOrEmpty(mapping.Pattern))
            {
                throw new ArgumentException("A pattern must be specified when using the --add or -a options");
            }

            return mapping;
        }
    }
}