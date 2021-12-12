using AdventOfCode2021.Solutions;
using NUnit.Framework;
using System;

namespace AdventOfCode2021.Tests
{
    public class Day12Tests
    {
        [Test]
        public void SolvePart1_SmallExample_ReturnsCorrectValue()
        {
            var day = new Day12(@"start-A
start-b
A-c
A-b
b-d
A-end
b-end");

            Assert.AreEqual(10, Convert.ToInt32(day.Part1));
        }

        [Test]
        public void SolvePart2_SmallExample_ReturnsCorrectValue()
        {
            var day = new Day12(@"start-A
start-b
A-c
A-b
b-d
A-end
b-end");

            Assert.AreEqual(36, Convert.ToInt64(day.Part2));
        }
    }
}