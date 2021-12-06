using AdventOfCode2021.Solutions;
using NUnit.Framework;
using System;

namespace AdventOfCode2021.Tests
{
    public class Day06Tests
    {
        [Test]
        public void SolvePart1_SmallExample_ReturnsCorrectValue()
        {
            var day = new Day06(@"3,4,3,1,2");

            Assert.AreEqual(5934, Convert.ToInt32(day.Part1));
        }

        [Test]
        public void SolvePart2_SmallExample_ReturnsCorrectValue()
        {
            var day = new Day06(@"3,4,3,1,2");

            Assert.AreEqual(26984457539, Convert.ToInt64(day.Part2));
        }
    }
}