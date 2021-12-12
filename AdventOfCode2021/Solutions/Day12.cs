using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Solutions
{
    public class Day12 : DayBase
    {
        public Day12(string input = null) : base(input)
        {

        }

        protected override object SolvePart1()
        {
            List<(string from, string to)> connections = Input.Select(i =>
            {
                var splitted = i.Split('-');
                return (from: splitted[0], to: splitted[1]);
            }).ToList();

            int numberOfPaths = 0;
            List<string> visited = new();
            FindNextCave("start", visited, connections, 0, ref numberOfPaths);

            return numberOfPaths;
        }



        protected override object SolvePart2()
        {
            List<(string from, string to)> connections = Input.Select(i =>
            {
                var splitted = i.Split('-');
                return (from: splitted[0], to: splitted[1]);
            }).ToList();

            int numberOfPaths = 0;
            List<string> visited = new();
            FindNextCave("start", visited, connections, 1, ref numberOfPaths);

            return numberOfPaths;
        }

        private void FindNextCave(string currentCave, List<string> visited, List<(string from, string to)> connections, int numberOfAllowedSecondVisitSmallCave, ref int numberOfPaths)
        {
            visited.Add(currentCave);

            if (visited.Where(v => v.Any(char.IsLower))
                       .GroupBy(x => x)
                       .Any(group => group.Count() > 2) ||
                visited.Where(v => v == "start").Count() == 2)
            {
                return;
            }

            var numberOfSecondVisitSmallCave = visited.Where(v => v.Any(char.IsLower))
                                                      .GroupBy(x => x)
                                                      .Where(group => group.Count() > 1)
                                                      .Count();

            if (numberOfAllowedSecondVisitSmallCave < numberOfSecondVisitSmallCave)
            {
                return;
            }

            if (currentCave == "end")
            {
                numberOfPaths++;
                return;
            }

            var nextCaves = connections.Where(c => c.from == currentCave).Select(c => c.to).ToList();
            nextCaves.AddRange(connections.Where(c => c.to == currentCave).Select(c => c.from).ToList());

            foreach (var nextCave in nextCaves)
            {
                if (visited.LastOrDefault() != nextCave)
                {
                    FindNextCave(nextCave, visited.ToList(), connections, numberOfAllowedSecondVisitSmallCave, ref numberOfPaths);
                }
            }
        }
    }
}
