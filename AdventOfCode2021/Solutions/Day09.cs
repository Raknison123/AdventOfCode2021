using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Solutions
{
    public class Day09 : DayBase
    {
        private Dictionary<(int x, int y), (int height, bool isLowest)> locations = new Dictionary<(int x, int y), (int height, bool isLowest)>();

        public Day09(string input = null) : base(input)
        {
            int y = 0;
            foreach (var row in Input)
            {
                for (int x = 0; x < row.Length; x++)
                {
                    locations.Add((x, y), (Convert.ToInt32(row[x].ToString()), false));
                }
                y++;
            }
            CalculateLowPoints();
        }

        protected override object SolvePart1()
        {
            return locations.Where(l => l.Value.isLowest).Sum(l => l.Value.height + 1);
        }

        protected override object SolvePart2()
        {
            var basins = new Dictionary<(int x, int y), Guid>();
            foreach (var lowPoint in locations.Where(l => l.Value.isLowest))
            {
                var guid = Guid.NewGuid();
                basins.Add((lowPoint.Key.x, lowPoint.Key.y), guid);
                AddNeighboursToBasin(lowPoint, basins, guid);
            }

            int product = 1;
            foreach (var count in basins.GroupBy(b => b.Value).OrderByDescending(g => g.Count()).Take(3).Select(b => b.Count()))
            {
                product *= count;
            }

            return product;
        }

        private void AddNeighboursToBasin(KeyValuePair<(int x, int y), (int height, bool isLowest)> lowPoint, Dictionary<(int x, int y), Guid> basins, Guid id)
        {
            var directions = new (int x, int y)[] { (0, -1), (0, 1), (1, 0), (-1, 0) };
            foreach (var direction in directions)
            {
                if (locations.TryGetValue((lowPoint.Key.x + direction.x, lowPoint.Key.y + direction.y), out (int height, bool isLowest) neighbour))
                {
                    if (neighbour.height < 9)
                    {
                        if (basins.TryAdd((lowPoint.Key.x + direction.x, lowPoint.Key.y + direction.y), id))
                        {
                            AddNeighboursToBasin(
                              lowPoint: new KeyValuePair<(int x, int y), (int height, bool isLowest)>((lowPoint.Key.x + direction.x, lowPoint.Key.y + direction.y), (neighbour.height, false)),
                              basins,
                              id);
                        }
                    }
                }
            }
        }

        private void CalculateLowPoints()
        {
            int maxX = locations.Max(l => l.Key.x);
            int maxY = locations.Max(l => l.Key.y);
            for (int y = 0; y <= maxY; y++)
            {
                for (int x = 0; x <= maxX; x++)
                {
                    var currentHeight = locations[(x, y)].height;
                    int lowest = int.MaxValue;
                    var directions = new (int x, int y)[] { (0, -1), (0, 1), (1, 0), (-1, 0) };

                    foreach (var direction in directions)
                    {
                        if (locations.ContainsKey((x + direction.x, y + direction.y)))
                        {
                            lowest = locations[(x + direction.x, y + direction.y)].height < lowest ?
                                     locations[(x + direction.x, y + direction.y)].height : lowest;
                        }
                    }

                    if (currentHeight < lowest)
                    {
                        locations[(x, y)] = (currentHeight, true);
                    }
                }
            }
        }
    }
}
