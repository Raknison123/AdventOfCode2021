using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Solutions
{
    public class Day11 : DayBase
    {
        private Dictionary<(int x, int y), int> octopuses = new Dictionary<(int x, int y), int>();

        public Day11(string input = null) : base(input)
        {
            int y = 0;
            foreach (var row in Input)
            {
                for (int x = 0; x < row.Length; x++)
                {
                    octopuses.Add((x, y), Convert.ToInt32(row[x].ToString()));
                }
                y++;
            }
        }

        protected override object SolvePart1()
        {
            var octopuses1 = octopuses.ToDictionary(o => o.Key, o => o.Value);
            int flashes = 0;

            for (int step = 0; step < 100; step++)
            {
                foreach (var octopKey in octopuses1.Keys)
                {
                    octopuses1[octopKey] = ++octopuses1[octopKey];
                }

                flashes += FlashOctopuses(octopuses1);
            }

            return flashes;
        }

        protected override object SolvePart2()
        {
            var octopuses2 = octopuses.ToDictionary(o => o.Key, o => o.Value);
            int step = 0;

            do
            {
                step++;
                foreach (var octopKey in octopuses2.Keys)
                {
                    octopuses2[octopKey] = ++octopuses2[octopKey];
                }

                FlashOctopuses(octopuses2);

            } while (!octopuses2.All(o => o.Value == 0));


            return step;
        }

        private int FlashOctopuses(Dictionary<(int x, int y), int> octopuses)
        {
            var directions = new (int x, int y)[] { (0, -1), (0, 1), (1, 0), (-1, 0), (-1, -1), (-1, 1), (1, 1), (1, -1) };
            var flashOctopuses = octopuses.Where(o => o.Value > 9).Select(o => o.Key).ToList();

            foreach (var flashOctopus in flashOctopuses)
            {
                foreach (var direction in directions)
                {
                    var coordinate = (flashOctopus.x + direction.x, flashOctopus.y + direction.y);
                    if (octopuses.TryGetValue(coordinate, out int neighbourOctopus))
                    {
                        if (neighbourOctopus > 0 && neighbourOctopus < 10)
                        {
                            octopuses[coordinate] = ++octopuses[coordinate];
                        }
                    }
                }

                octopuses[(flashOctopus.x, flashOctopus.y)] = 0;
            }

            if (octopuses.Any(o => o.Value == 10))
            {
                return FlashOctopuses(octopuses) + flashOctopuses.Count;
            }

            return flashOctopuses.Count;
        }
    }
}
