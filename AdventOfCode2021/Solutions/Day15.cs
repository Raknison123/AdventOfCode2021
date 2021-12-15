using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode2021.Solutions
{
    public class Day15 : DayBase
    {
        public Day15(string input = null) : base(input)
        {

        }

        protected override object SolvePart1()
        {
            return FindShortestPathCostsDijkstra(AddTilesToPositions(1, 1));
        }

        protected override object SolvePart2()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var result = FindShortestPathCostsDijkstra(AddTilesToPositions(5, 5));
            Console.WriteLine($"Elapsed:{stopWatch.ElapsedMilliseconds}");
            return result;
        }

        private Dictionary<(int x, int y), Node> AddTilesToPositions(int right, int down)
        {
            var positions = new Dictionary<(int x, int y), Node>();
            for (int i = 0; i < right; i++)
            {
                for (int j = 0; j < down; j++)
                {
                    for (int y = 0; y < Input.Length; y++)
                    {
                        for (int x = 0; x < Input[y].Length; x++)
                        {
                            var newX = Input[y].Length * i + x;
                            var newY = Input.Length * j + y;

                            var costNotNormalized = Convert.ToInt32(Input[y][x].ToString()) + i + j;
                            var cost = costNotNormalized / 10 + costNotNormalized % 10;
                            positions.Add((newX, newY), new Node
                            {
                                Cost = cost,
                                IsVisited = false,
                                TotalCost = int.MaxValue,
                                X = newX,
                                Y = newY,
                            });
                        }
                    }
                }
            }

            return positions;
        }

        private static int FindShortestPathCostsDijkstra(Dictionary<(int x, int y), Node> positions)
        {
            var prioQueue = new PriorityQueue<Node, int>();
            var startPoint = positions.First().Value;
            var endPoint = positions.Last().Value;
            startPoint.TotalCost = 0;
            prioQueue.Enqueue(startPoint, 0);

            while (prioQueue.Count > 0)
            {
                var currentNode = prioQueue.Dequeue();

                if (currentNode.X == endPoint.X && currentNode.Y == endPoint.Y)
                {
                    return currentNode.TotalCost;
                }

                var directions = new (int x, int y)[]
                {
                    (currentNode.X + 1, currentNode.Y),
                    (currentNode.X, currentNode.Y + 1),
                    (currentNode.X, currentNode.Y - 1),
                    (currentNode.X - 1, currentNode.Y)
                };

                foreach (var direction in directions)
                {
                    if (positions.ContainsKey(direction))
                    {
                        var neighbourNode = positions[direction];
                        if ((currentNode.TotalCost + neighbourNode.Cost) < neighbourNode.TotalCost)
                        {
                            neighbourNode.TotalCost = currentNode.TotalCost + neighbourNode.Cost;
                            neighbourNode.PreviousNode = currentNode;
                        }

                        if (neighbourNode.IsVisited || neighbourNode.IsQueued)
                        {
                            continue;
                        }

                        neighbourNode.IsQueued = true;
                        prioQueue.Enqueue(neighbourNode, neighbourNode.TotalCost);
                    }
                }

                currentNode.IsVisited = true;
            }

            return int.MaxValue;
        }
    }

    public class Node
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Node PreviousNode { get; set; }
        public int Cost { get; set; }
        public int TotalCost { get; set; }
        public bool IsVisited { get; set; }
        public bool IsQueued { get; set; }
    }
}
