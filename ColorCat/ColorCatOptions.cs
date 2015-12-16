using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ColorCat
{
    public class ColorCatOptions
    {
        [Option('a', "add", HelpText = "Add a color mapping to the configuration")]
        public bool Add { get; set; }

        [Option('c', "color", HelpText = "When adding a color mapping, the color to use for the mapping.  Requires --add to be specified.")]
        public ConsoleColor Color { get; set; }

        [Option('i', "ignoreCase", HelpText = "When adding a color mapping, ignore case when executing the mapping.  Requires --add to be specified.")]
        public bool IgnoreCase { get; set; }

        [Option('r', "regex", HelpText = "When adding a color mapping, execute mapping as a regular expression instead of a string compare.  Requires --add to be specified.")]
        public bool IsRegex { get; set; }

        [ValueList(typeof(List<string>))]
        public IList<string> Pattern { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            var asm = typeof(ColorCatOptions).Assembly;

            var help = new HelpText
            {
                Heading = new HeadingInfo(asm.GetName().Name, asm.GetName().Version.ToString()),
                AdditionalNewLineAfterOption = true,
                AddDashesToOption = true
            };

            AddCopyright(help);
            help.AddOptions(this);
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
    }
}