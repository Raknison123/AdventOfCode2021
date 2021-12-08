using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Solutions
{
    public class Day08 : DayBase
    {
        private List<(string[] patterns, string[] outputValues)> digits = new List<(string[] patterns, string[] outputValues)>();

        public Day08(string input = null) : base(input)
        {
            var splitted = Input.Select(x => x.Split(new string[] { " | " }, StringSplitOptions.None)).ToList();
            foreach (var part in splitted)
            {
                var patterns = part[0].Split(' ');
                var outputValues = part[1].Split(' ');
                digits.Add((patterns, outputValues));
            }
        }

        protected override object SolvePart1()
        {
            int sum = 0;
            foreach (var digit in digits)
            {
                sum += digit.outputValues.Count(o => o.Length == 2 || o.Length == 3 || o.Length == 4 || o.Length == 7);
            }

            return sum;
        }

        protected override object SolvePart2()
        {
            int sum = 0;
            foreach (var digit in digits)
            {
                Dictionary<int, string> wireMapping = FillWireMapping(digit.patterns);
                var stringNumber = string.Empty;

                foreach (var outputValue in digit.outputValues)
                {
                    stringNumber += wireMapping.Single(m => !m.Value.Except(outputValue).Any() && !outputValue.Except(m.Value).Any()).Key.ToString();
                }

                sum += Convert.ToInt32(stringNumber);
            }

            return sum;
        }

        private static Dictionary<int, string> FillWireMapping(string[] patterns)
        {
            var wireMapping = new Dictionary<int, string>();
            foreach (var pattern in patterns.OrderBy(d => d.Length))
            {
                if (pattern.Length == 2)
                {
                    wireMapping.Add(1, pattern);
                }
                else if (pattern.Length == 3)
                {
                    wireMapping.Add(7, pattern);
                }
                else if (pattern.Length == 4)
                {
                    wireMapping.Add(4, pattern);
                }
                else if (pattern.Length == 7)
                {
                    wireMapping.Add(8, pattern);
                }
                else if (pattern.Length == 6 && pattern.Intersect(wireMapping[4]).Count() < wireMapping[4].Length &&
                        pattern.Intersect(wireMapping[1]).Count() == wireMapping[1].Length)
                {
                    // It's a zero!
                    wireMapping.Add(0, pattern);
                }
                else if (pattern.Length == 5 && pattern.Intersect(wireMapping[4]).Count() == 2 &&
                        pattern.Intersect(wireMapping[1]).Count() < wireMapping[1].Length)
                {
                    // It's a two!
                    wireMapping.Add(2, pattern);
                }
                else if (pattern.Length == 5 && pattern.Intersect(wireMapping[1]).Count() == wireMapping[1].Length)
                {
                    // It's a three!
                    wireMapping.Add(3, pattern);
                }
                else if (pattern.Length == 5 && pattern.Intersect(wireMapping[1]).Count() < wireMapping[1].Length &&
                         pattern.Intersect(wireMapping[4]).Count() == 3)
                {
                    // It's a five!
                    wireMapping.Add(5, pattern);
                }
                else if (pattern.Length == 6 && pattern.Intersect(wireMapping[4]).Count() < wireMapping[4].Length &&
                         pattern.Intersect(wireMapping[1]).Count() < wireMapping[1].Length)
                {
                    // It's a six!
                    wireMapping.Add(6, pattern);
                }
                else if (pattern.Length == 6 && pattern.Intersect(wireMapping[4]).Count() == wireMapping[4].Length)
                {
                    // It's a nine!
                    wireMapping.Add(9, pattern);
                }
            }

            return wireMapping;
        }
    }
}
