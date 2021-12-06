using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Solutions
{
    public class Day06 : DayBase
    {
        public Day06(string input = null) : base(input)
        {

        }

        protected override object SolvePart1()
        {
            return GetCountOfReproducedFishes(80);
        }

        protected override object SolvePart2()
        {
            return GetCountOfReproducedFishes(256);
        }

        private long GetCountOfReproducedFishes(int days)
        {
            List<int> currentFishes = InputComplete.Split(',').Select(x => Convert.ToInt32(x)).ToList();
            long[] countOfFishesByIndex = new long[9];

            // Key is to store the count of fishes by index instead of creating a single record for each fish
            foreach (var index in currentFishes)
            {
                countOfFishesByIndex[index]++;
            }

            long[] tempCountOfFishesByIndex = countOfFishesByIndex.ToArray();

            for (int day = 0; day < days; day++)
            {
                for (int index = 0; index < countOfFishesByIndex.Length; index++)
                {
                    long fishCount = countOfFishesByIndex[index];
                    if (index == 0)
                    {
                        tempCountOfFishesByIndex[0] -= fishCount;
                        tempCountOfFishesByIndex[6] += fishCount;
                        tempCountOfFishesByIndex[8] += fishCount;
                    }
                    else
                    {
                        tempCountOfFishesByIndex[index] -= fishCount;
                        tempCountOfFishesByIndex[index - 1] += fishCount;
                    }
                }

                countOfFishesByIndex = tempCountOfFishesByIndex.ToArray();
            }

            return countOfFishesByIndex.Sum(x => x);
        }
    }
}
