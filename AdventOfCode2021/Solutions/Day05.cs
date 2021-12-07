using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Solutions
{
    public class Day05 : DayBase
    {
        private List<(int x1, int y1, int x2, int y2)> ventlines;

        public Day05(string input = null) : base(input)
        {
            ventlines = new List<(int x1, int y1, int x2, int y2)>();

            for (int i = 0; i < Input.Length; i++)
            {
                var splitted = Input[i].Split(new string[] { " -> " }, StringSplitOptions.None);
                var splittedStart = splitted[0].Split(',');
                var splittedEnd = splitted[1].Split(',');
                ventlines.Add((Convert.ToInt32(splittedStart[0]), Convert.ToInt32(splittedStart[1]), Convert.ToInt32(splittedEnd[0]), Convert.ToInt32(splittedEnd[1])));
            }
        }

        protected override object SolvePart1()
        {
            Dictionary<(int x, int y), int> oceanFloor = DrawLines(false);
            return oceanFloor.Count(o => o.Value >= 2);
        }

        protected override object SolvePart2()
        {
            Dictionary<(int x, int y), int> oceanFloor = DrawLines(true);
            return oceanFloor.Count(o => o.Value >= 2);
        }

        private Dictionary<(int x, int y), int> DrawLines(bool considerDiagonal)
        {
            Dictionary<(int x, int y), int> oceanFloor = new Dictionary<(int x, int y), int>();

            foreach (var ventLine in ventlines)
            {
                bool isDiagonal = !(ventLine.x1 == ventLine.x2 || ventLine.y1 == ventLine.y2);
                if (isDiagonal && !considerDiagonal)
                {
                    continue;
                }

                if (isDiagonal)
                {
                    int y = ventLine.y1;
                    if (ventLine.x1 > ventLine.x2)
                    {
                        for (int x = ventLine.x1; x >= ventLine.x2; x--)
                        {
                            DrawOceanFloor(oceanFloor, y, x);
                            IncreaseDecreaseY(ventLine, ref y);
                        }
                    }
                    else
                    {
                        for (int x = ventLine.x1; x <= ventLine.x2; x++)
                        {
                            DrawOceanFloor(oceanFloor, y, x);
                            IncreaseDecreaseY(ventLine, ref y);
                        }
                    }
                }
                else
                {
                    int startX = ventLine.x1 < ventLine.x2 ? ventLine.x1 : ventLine.x2;
                    int endX = ventLine.x1 < ventLine.x2 ? ventLine.x2 : ventLine.x1;
                    int startY = ventLine.y1 < ventLine.y2 ? ventLine.y1 : ventLine.y2;
                    int endY = ventLine.y1 < ventLine.y2 ? ventLine.y2 : ventLine.y1;

                    for (int x = startX; x <= endX; x++)
                    {
                        for (int y = startY; y <= endY; y++)
                        {
                            DrawOceanFloor(oceanFloor, y, x);
                        }
                    }
                }
            }

            return oceanFloor;
        }

        private static void DrawOceanFloor(Dictionary<(int x, int y), int> oceanFloor, int y, int x)
        {
            if (oceanFloor.ContainsKey((x, y)))
            {
                oceanFloor[(x, y)]++;
            }
            else
            {
                oceanFloor.Add((x, y), 1);
            }
        }

        private static void IncreaseDecreaseY((int x1, int y1, int x2, int y2) ventLine, ref int y)
        {
            if (ventLine.y1 > ventLine.y2)
            {
                y--;
            }
            else
            {
                y++;
            }
        }
    }
}
