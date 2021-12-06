using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Solutions
{
    public class Day01 : DayBase
    {
        public Day01(string input = null) : base(input)
        {
        }
        protected override object SolvePart1()
        {
            List<int> inputs = Input.Select(x => Convert.ToInt32(x)).ToList();

            return GetNumberOfIncreasesFromPrevious(inputs);
        }

        protected override object SolvePart2()
        {
            var slidingWindowSum = new List<int>();
            for (int i = 2; i < Input.Length; i++)
            {
                slidingWindowSum.Add(Convert.ToInt32(Input[i - 2]) + Convert.ToInt32(Input[i - 1]) + Convert.ToInt32(Input[i]));
            }

            return GetNumberOfIncreasesFromPrevious(slidingWindowSum);
        }

        private static int GetNumberOfIncreasesFromPrevious(List<int> inputs)
        {
            var numOfIncreases = 0;
            var result = inputs.Aggregate((a, b) =>
            {
                if (b > a)
                {
                    numOfIncreases++;
                }
                return b;
            });

            return numOfIncreases;
        }
    }
}
