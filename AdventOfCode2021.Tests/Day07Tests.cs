using AdventOfCode2021.Solutions;
using NUnit.Framework;
using System;

namespace AdventOfCode2021.Tests
{
    public class Day07Tests
    {
        [Test]
        public void SolvePart1_SmallExample_ReturnsCorrectValue()
        {
            var day = new Day07(@"16,1,2,0,4,2,7,1,2,14");

            Assert.AreEqual(37, Convert.ToInt32(day.Part1));
        }

        [Test]
        public void SolvePart2_SmallExample_ReturnsCorrectValue()
        {
            var day = new Day07(@"16,1,2,0,4,2,7,1,2,14");

            Assert.AreEqual(168, Convert.ToInt64(day.Part2));
        }
    }
}