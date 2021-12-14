using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AdventOfCode2021.Solutions
{
    public class Day14 : DayBase
    {
        public Day14(string input = null) : base(input)
        {

        }

        protected override object SolvePart1()
        {
            return CreatePolymer(10);
        }

        protected override object SolvePart2()
        {
            return CreatePolymer(40);
        }

        private object CreatePolymer(int steps)
        {
            var template = Input.First();
            var pairs = Input.Skip(2).Select(p => p.Split(" -> ")).ToDictionary(p => p[0], p => p[1]);

            Dictionary<string, long> polymer = new();
            for (int i = 0; i < template.Length; i++)
            {
                var connection = template.Substring(i, i < template.Length - 1 ? 2 : 1);
                if (polymer.ContainsKey(connection))
                {
                    polymer[connection]++;
                }
                else
                {
                    polymer.Add(connection, 1);
                }
            }

            for (int step = 0; step < steps; step++)
            {
                Dictionary<string, long> workingPolymer = polymer.ToDictionary(p => p.Key, p => p.Value);
                foreach (var connection in polymer)
                {
                    if (pairs.TryGetValue(connection.Key, out string pair))
                    {
                        if (polymer.ContainsKey(connection.Key))
                        {
                            var existingCount = polymer[connection.Key];
                            workingPolymer[connection.Key] -= existingCount;

                            TryAddOrSumup(workingPolymer, existingCount, connection.Key[0] + pair);
                            TryAddOrSumup(workingPolymer, existingCount, pair + connection.Key[1]);
                        }
                        else
                        {
                            workingPolymer.Add(connection.Key, 1);
                        }
                    }
                }

                polymer = workingPolymer.ToDictionary(p => p.Key, p => p.Value);
            }

            var maxCharOccurs = polymer.GroupBy(x => x.Key[0]).Select(x => x.Sum(g => g.Value)).Max();
            var minCharOccurs = polymer.GroupBy(x => x.Key[0]).Select(x => x.Sum(g => g.Value)).Min();

            return maxCharOccurs - minCharOccurs;
        }

        private static void TryAddOrSumup(Dictionary<string, long> workingPolymer, long existingCount, string insertConnection)
        {
            if (workingPolymer.ContainsKey(insertConnection))
            {
                workingPolymer[insertConnection] += existingCount;
            }
            else
            {
                workingPolymer.Add(insertConnection, existingCount);
            }
        }
    }
}
