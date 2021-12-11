using AdventOfCode2021.Solutions;
using NUnit.Framework;
using System;

namespace AdventOfCode2021.Tests
{
    public class Day10Tests
    {
        [Test]
        public void SolvePart1_SmallExample_ReturnsCorrectValue()
        {
            var day = new Day10(@"[({(<(())[]>[[{[]{<()<>>
[(()[<>])]({[<{<<[]>>(
{([(<{}[<>[]}>{[]{[(<()>
(((({<>}<{<{<>}{[]{[]{}
[[<[([]))<([[{}[[()]]]
[{[{({}]{}}([{[{{{}}([]
{<[[]]>}<{[{[{[]{()[[[]
[<(<(<(<{}))><([]([]()
<{([([[(<>()){}]>(<<{{
<{([{{}}[<[[[<>{}]]]>[]]");

            Assert.AreEqual(26397, Convert.ToInt32(day.Part1));
        }

        [Test]
        public void SolvePart2_SmallExample_ReturnsCorrectValue()
        {
            var day = new Day10(@"[({(<(())[]>[[{[]{<()<>>
[(()[<>])]({[<{<<[]>>(
{([(<{}[<>[]}>{[]{[(<()>
(((({<>}<{<{<>}{[]{[]{}
[[<[([]))<([[{}[[()]]]
[{[{({}]{}}([{[{{{}}([]
{<[[]]>}<{[{[{[]{()[[[]
[<(<(<(<{}))><([]([]()
<{([([[(<>()){}]>(<<{{
<{([{{}}[<[[[<>{}]]]>[]]");

            Assert.AreEqual(288957, Convert.ToInt64(day.Part2));
        }
    }
}