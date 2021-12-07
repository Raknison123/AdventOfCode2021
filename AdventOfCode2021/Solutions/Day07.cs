using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Solutions
{
    public class Day07 : DayBase
    {
        private List<int> crabPositions;

        public Day07(string input = null) : base(input)
        {
            crabPositions = InputComplete.Split(',').Select(x => Convert.ToInt32(x)).ToList();
        }

        protected override object SolvePart1()
        {
            long cheapestFuelOutcome = int.MaxValue;
            for (int targetPos = 0; targetPos < crabPositions.Max(); targetPos++)
            {
                long fuelSteps = AlignCrabsOnPositionPart1(crabPositions, targetPos);
                if (cheapestFuelOutcome > fuelSteps)
                {
                    cheapestFuelOutcome = fuelSteps;
                }
            }

            return cheapestFuelOutcome;
        }

        private long AlignCrabsOnPositionPart1(List<int> crabPositions, int targetPosition)
        {
            long totalFuel = 0;
            foreach (var currentPos in crabPositions)
            {
                totalFuel += Math.Abs(currentPos - targetPosition);
            }

            return totalFuel;
        }

        protected override object SolvePart2()
        {
            long cheapestFuelOutcome = int.MaxValue;
            for (int targetPos = 0; targetPos < crabPositions.Max(); targetPos++)
            {
                long fuelSteps = AlignCrabsOnPositionPart2(crabPositions, targetPos);
                if (cheapestFuelOutcome > fuelSteps)
                {
                    cheapestFuelOutcome = fuelSteps;
                }
            }

            return cheapestFuelOutcome;
        }

        private long AlignCrabsOnPositionPart2(List<int> crabPositions, int targetPosition)
        {
            long totalFuel = 0;
            foreach (var currentPos in crabPositions)
            {
                int diff = Math.Abs(currentPos - targetPosition);
                for (int cost = 1; cost <= diff; cost++)
                {
                    totalFuel += cost;
                }
            }

            return totalFuel;
        }
    }
}
