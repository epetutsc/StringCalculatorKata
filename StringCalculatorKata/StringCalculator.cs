using StringCalculatorKata.Exceptions;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringCalculatorKata
{
    public class StringCalculator
    {
        public int Add(string numbers)
        {
            if (string.IsNullOrWhiteSpace(numbers))
            {
                return 0;
            }

            var lines = numbers.Split('\n');
            var (delimiters, skip) = GetDelimiters(lines);
            if (skip)
            {
                numbers = string.Join("\n", lines.Skip(1));
            }

            var integers = numbers.Split(delimiters, System.StringSplitOptions.None)
                .Select(int.Parse)
                .Where(i => i <= 1000);

            var invalid = integers.Where(i => i < 0).ToList();
            if (invalid.Any())
            {
                throw new NegativesNotAllowedException($"Negatives not allowed: {string.Join(",", invalid)}");
            }

            return integers.Sum();
        }

        private static (string[] delimiters, bool skipFirstLine) GetDelimiters(string[] lines)
        {
            var delimiters = new[]
            {
                ",", "\n"
            };

            if (lines.Length < 1)
            {
                return (delimiters, false);
            }

            var line = lines[0];
            if (!line.StartsWith("//"))
            {
                return (delimiters, false);
            }

            if (line.Contains("[") && line.Contains("]"))
            {
                var matches = Regex.Matches(line, @"\[[^\[]+\]");
                delimiters = matches
                    .Select(m => m.Groups.Values.Select(g => g.Value.Trim('[', ']')))
                    .SelectMany(o => o)
                    .Union(new[] { "\n" })
                    .Distinct()
                    .ToArray();

                return (delimiters, true);
            }

            delimiters = new[]
            {
                line.Trim('/', '[', ']'),
                "\n"
            };

            return (delimiters, true);
        }
    }
}
