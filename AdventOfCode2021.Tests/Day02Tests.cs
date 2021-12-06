using AdventOfCode2021.Solutions;
using NUnit.Framework;
using System;

namespace AdventOfCode2021.Tests
{
    public class Day02Tests
    {
        [Test]
        public void SolvePart1_SmallExample_ReturnsCorrectValue()
        {
            var day02 = new Day02(@"forward 5
down 5
forward 8
up 3
down 8
forward 2");

            Assert.AreEqual(150, Convert.ToInt32(day02.Part1));
        }

        [Test]
        public void SolvePart2_SmallExample_ReturnsCorrectValue()
        {
            var day02 = new Day02(@"forward 5
down 5
forward 8
up 3
down 8
forward 2");

            Assert.AreEqual(900, Convert.ToInt32(day02.Part2));
        }
    }
}