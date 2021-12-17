using AdventOfCode2021.Solutions;
using NUnit.Framework;
using System;

namespace AdventOfCode2021.Tests
{
    public class Day17Tests
    {
        [Test]
        public void SolvePart1_SmallExample_ReturnsCorrectValue()
        {
            var dayA = new Day17(@"target area: x=20..30, y=-10..-5");
            Assert.AreEqual(45, Convert.ToInt32(dayA.Part1));
        }

        [Test]
        public void SolvePart2_SmallExample_ReturnsCorrectValue()
        {
            var dayA = new Day17(@"target area: x=20..30, y=-10..-5");
            Assert.AreEqual(112, Convert.ToInt64(dayA.Part2));
        }
    }
}