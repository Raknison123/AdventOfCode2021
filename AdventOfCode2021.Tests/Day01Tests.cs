using AdventOfCode2021.Solutions;
using NUnit.Framework;
using System;

namespace AdventOfCode2021.Tests
{
    public class Day01Tests
    {
        [Test]
        public void SolvePart1_SmallExample_ReturnsCorrectValue()
        {
            var day01 = new Day01(@"199
200
208
210
200
207
240
269
260
263");

            Assert.AreEqual(7, Convert.ToInt32(day01.Part1));
        }

        [Test]
        public void SolvePart2_SmallExample_ReturnsCorrectValue()
        {
            var day01 = new Day01(@"199
200
208
210
200
207
240
269
260
263");

            Assert.AreEqual(5, Convert.ToInt32(day01.Part2));
        }
    }
}