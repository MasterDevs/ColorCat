using NUnit.Framework;

namespace ColorCat.Tests
{
    [TestFixture]
    public class ColorMappingTests

    {
        [Test]
        [TestCase("Error", "error", false, ExpectedResult = false)]
        [TestCase("Error", "error", true, ExpectedResult = true)]
        [TestCase("error", "error", false, ExpectedResult = true)]
        [TestCase("error", "error", true, ExpectedResult = true)]
        [TestCase("error", "or", false, ExpectedResult = true)]
        [TestCase("error", ".*", false, ExpectedResult = false)]
        [TestCase("!@#$%^&*()_+", "!@#$%^&*()_+", false, ExpectedResult = true)]
        public bool IsMatch_NotRegex(string text, string pattern, bool ignoreCase)
        {
            // Assemble
            var mapping = new ColorMapping
            {
                IsRegex = false,
                IgnoreCase = ignoreCase,
                Pattern = pattern,
            };

            // Act/Assert
            return mapping.IsMatch(text);
        }

        [Test]
        [TestCase("Error", "error", false, ExpectedResult = false)]
        [TestCase("Error", "error", true, ExpectedResult = true)]
        [TestCase("error", "error", false, ExpectedResult = true)]
        [TestCase("error", "error", true, ExpectedResult = true)]
        [TestCase("error", ".*", false, ExpectedResult = true)]
        [TestCase("error", "or|xyz", false, ExpectedResult = true)]
        [TestCase("error", "abc|xyz", false, ExpectedResult = false)]
        public bool IsMatch_Regex(string text, string pattern, bool ignoreCase)
        {
            // Assemble
            var mapping = new ColorMapping
            {
                IsRegex = true,
                IgnoreCase = ignoreCase,
                Pattern = pattern,
            };

            // Act/Assert
            return mapping.IsMatch(text);
        }
    }
}