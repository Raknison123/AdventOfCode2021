using AdventOfCode2021.Solutions;
using NUnit.Framework;
using System;

namespace AdventOfCode2021.Tests
{
    public class Day09Tests
    {
        [Test]
        public void SolvePart1_SmallExample_ReturnsCorrectValue()
        {
            var day = new Day09(@"2199943210
3987894921
9856789892
8767896789
9899965678");

            Assert.AreEqual(15, Convert.ToInt32(day.Part1));
        }

        [Test]
        public void SolvePart2_SmallExample_ReturnsCorrectValue()
        {
            var day = new Day09(@"2199943210
3987894921
9856789892
8767896789
9899965678");

            Assert.AreEqual(1134, Convert.ToInt64(day.Part2));
        }
    }
}