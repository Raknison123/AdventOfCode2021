using AdventOfCode2021.Solutions;
using NUnit.Framework;
using System;

namespace AdventOfCode2021.Tests
{
    public class Day14Tests
    {
        [Test]
        public void SolvePart1_SmallExample_ReturnsCorrectValue()
        {
            var day = new Day14(@"NNCB

CH -> B
HH -> N
CB -> H
NH -> C
HB -> C
HC -> B
HN -> C
NN -> C
BH -> H
NC -> B
NB -> B
BN -> B
BB -> N
BC -> B
CC -> N
CN -> C");

            Assert.AreEqual(1588, Convert.ToInt32(day.Part1));
        }

        [Test]
        public void SolvePart2_SmallExample_ReturnsCorrectValue()
        {
            var day = new Day14(@"NNCB

CH -> B
HH -> N
CB -> H
NH -> C
HB -> C
HC -> B
HN -> C
NN -> C
BH -> H
NC -> B
NB -> B
BN -> B
BB -> N
BC -> B
CC -> N
CN -> C");

            Assert.AreEqual(2188189693529, Convert.ToInt64(day.Part2));
        }
    }
}