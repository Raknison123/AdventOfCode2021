using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Solutions
{
    public class Day10 : DayBase
    {
        private IEnumerable<char[]> chunks;
        private List<char> openCharacters = new List<char> { '[', '{', '<', '(' };
        private List<char> closeCharacters = new List<char> { ']', '}', '>', ')' };
        private Dictionary<char, int> pointsTableA = new Dictionary<char, int> { { ')', 3 }, { ']', 57 }, { '}', 1197 }, { '>', 25137 } };
        private Dictionary<char, int> pointsTableB = new Dictionary<char, int> { { ')', 1 }, { ']', 2 }, { '}', 3 }, { '>', 4 } };

        public Day10(string input = null) : base(input)
        {
            chunks = Input.Select(x => x.ToCharArray());
        }

        protected override object SolvePart1()
        {
            var openChunkPositions = new List<int>();
            int points = 0;

            foreach (var chunk in chunks)
            {
                for (int i = 0; i < chunk.Length; i++)
                {
                    if (openCharacters.Contains(chunk[i]))
                    {
                        openChunkPositions.Add(openCharacters.IndexOf(chunk[i]));
                    }
                    else if (closeCharacters.IndexOf(chunk[i]) == openChunkPositions.Last())
                    {
                        openChunkPositions.RemoveAt(openChunkPositions.Count - 1);
                    }
                    else
                    {
                        points += pointsTableA[chunk[i]];
                        break;
                    }
                }
            }

            return points;
        }

        protected override object SolvePart2()
        {
            List<long> scores = new List<long>();
            foreach (var chunk in chunks)
            {
                var openChunkPositions = new List<int>();
                bool isIncomplete = true;
                for (int i = 0; i < chunk.Length; i++)
                {
                    if (openCharacters.Contains(chunk[i]))
                    {
                        openChunkPositions.Add(openCharacters.IndexOf(chunk[i]));
                    }
                    else if (closeCharacters.IndexOf(chunk[i]) == openChunkPositions.Last())
                    {
                        openChunkPositions.RemoveAt(openChunkPositions.Count - 1);
                    }
                    else
                    {
                        isIncomplete = false;
                        break;
                    }
                }

                if (isIncomplete)
                {
                    long score = 0;
                    openChunkPositions.Reverse();
                    foreach (var pos in openChunkPositions)
                    {
                        long point = pointsTableB[closeCharacters[pos]];
                        score = (score * 5) + point;
                    }

                    scores.Add(score);
                }
            }

            return scores.OrderBy(x => x).ToList()[scores.Count / 2];
        }
    }
}