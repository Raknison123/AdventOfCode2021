using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Solutions
{
    public class Day17 : DayBase
    {
        private int targetXFrom;
        private int targetXTo;
        private int targetYFrom;
        private int targetYTo;

        public Day17(string input = null) : base(input)
        {
            var xySplitted = InputComplete.Split(", ");
            var xSplitted = xySplitted[0].Replace("target area: x=", string.Empty).Split("..");
            var ySplitted = xySplitted[1].Replace("y=", string.Empty).Split("..");

            targetXFrom = Convert.ToInt32(xSplitted[0]);
            targetXTo = Convert.ToInt32(xSplitted[1]);
            targetYFrom = Convert.ToInt32(ySplitted[1]);
            targetYTo = Convert.ToInt32(ySplitted[0]);
        }

        protected override object SolvePart1()
        {
            List<(int x, int y, int highestY)> results = CalculatePossibleTrajectories(targetXFrom, targetXTo, targetYFrom, targetYTo);
            return results.OrderByDescending(r => r.highestY).First().highestY;
        }

        protected override object SolvePart2()
        {
            List<(int x, int y, int highestY)> results = CalculatePossibleTrajectories(targetXFrom, targetXTo, targetYFrom, targetYTo);
            return results.Count;
        }

        private List<(int x, int y, int highestY)> CalculatePossibleTrajectories(int targetXFrom, int targetXTo, int targetYFrom, int targetYTo)
        {
            var results = new List<(int x, int y, int highestY)>();
            for (int y = -200; y < 200; y++)
            {
                for (int x = 0; x < 100; x++)
                {
                    var velocity = (x, y);
                    var highestY = ThrowProbe(targetXFrom, targetXTo, targetYFrom, targetYTo, velocity);
                    if (highestY.HasValue)
                    {
                        results.Add((velocity.x, velocity.y, highestY.Value));
                    }
                }
            }

            return results;
        }

        private int? ThrowProbe(int targetXFrom, int targetXTo, int targetYFrom, int targetYTo, (int x, int y) velocity)
        {
            var probePos = (x: 0, y: 0);
            var reachedYTrajectory = new List<int>();
            while (probePos.x <= targetXTo && probePos.y >= targetYTo)
            {
                probePos = (probePos.x + velocity.x, probePos.y + velocity.y);
                reachedYTrajectory.Add(probePos.y);
                velocity.x = velocity.x > 0 ? --velocity.x : velocity.x < 0 ? ++velocity.x : velocity.x;
                velocity.y = --velocity.y;

                if (HasHitTarget(probePos, targetXFrom, targetXTo, targetYFrom, targetYTo))
                {
                    return reachedYTrajectory.Max();
                }
            }

            return null;
        }

        private bool HasHitTarget((int x, int y) probePos, int targetXFrom, int targetXTo, int targetYFrom, int targetYTo)
        {
            return probePos.x >= targetXFrom &&
                   probePos.x <= targetXTo &&
                   probePos.y <= targetYFrom &&
                   probePos.y >= targetYTo;
        }
    }
}