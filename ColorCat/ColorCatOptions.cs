using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ColorCat
{
    public class ColorCatOptions
    {
        [VerbOption("add", HelpText = "Add a color mapping to the configuration")]
        public AddOption Add { get; set; }

        [HelpOption]
        [HelpVerbOption]
        public string GetUsage(string verb)
        {
            var help = HelpText.AutoBuild(this, verb);

            help.AdditionalNewLineAfterOption = true;

            switch (verb)
            {
                case null:
                    help.AddPostOptionsLine(string.Empty);
                    help.AddPostOptionsLine("For more information on a specific command use --help");
                    help.AddPostOptionsLine("Example:  colorCat add --help");
                    help.AddPostOptionsLine(string.Empty);
                    break;

                case "add":

                    help.AddPostOptionsLine("Valid colorss are:  ");
                    help.AddPostOptionsLine(string.Empty);
                    foreach (var color in Enum.GetNames(typeof(ConsoleColor)))
                    {
                        help.AddPostOptionsLine($"  {color}");
                    }

                    help.AddPostOptionsLine(string.Empty);
                    help.AddPostOptionsLine("Example:");
                    help.AddPostOptionsLine("  colorCat add -i -c red error");
                    help.AddPostOptionsLine(string.Empty);
                    break;
            }

            if (string.IsNullOrEmpty(verb))
            {
                help.AddDashesToOption = false;
            }
            else
            {
                help.AddDashesToOption = true;
                help.AddPreOptionsLine(verb);
            }

            return help;
        }

        private static void AddCopyright(HelpText help)
        {
            var currentAssem = typeof(ColorCatOptions).Assembly;
            object[] attribs = currentAssem.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), true);
            if (attribs.Length > 0)
            {
                var copyright = ((AssemblyCopyrightAttribute)attribs[0]).Copyright;
                help.AddPreOptionsLine(copyright);
            }
        }

        public class AddOption
        {
            [Option('c', "color", Required = true, HelpText = "The color to use for the mapping.")]
            public ConsoleColor Color { get; set; }

            [Option('i', "ignoreCase", HelpText = "Ignore case when executing the mapping.")]
            public bool IgnoreCase { get; set; }

            [Option('r', "regex", HelpText = "Execute mapping as a regular expression instead of a string compare.")]
            public bool IsRegex { get; set; }

            [ValueList(typeof(List<string>))]
            public IList<string> Pattern { get; set; }
        }
    }
}