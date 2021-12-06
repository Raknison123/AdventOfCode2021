using AdventOfCode2021.Solutions;
using NUnit.Framework;
using System;

namespace AdventOfCode2021.Tests
{
    public class Day03Tests
    {
        [Test]
        public void SolvePart1_SmallExample_ReturnsCorrectValue()
        {
            var day = new Day03(@"00100
11110
10110
10111
10101
01111
00111
11100
10000
11001
00010
01010");

            Assert.AreEqual(198, Convert.ToInt32(day.Part1));
        }

        [Test]
        public void SolvePart2_SmallExample_ReturnsCorrectValue()
        {
            var day = new Day03(@"00100
11110
10110
10111
10101
01111
00111
11100
10000
11001
00010
01010");

            Assert.AreEqual(230, Convert.ToInt32(day.Part2));
        }
    }
}