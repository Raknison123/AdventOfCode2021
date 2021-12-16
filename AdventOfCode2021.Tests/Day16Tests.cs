using AdventOfCode2021.Solutions;
using NUnit.Framework;
using System;

namespace AdventOfCode2021.Tests
{
    public class Day16Tests
    {
        [Test]
        public void SolvePart1_SmallExample_ReturnsCorrectValue()
        {
            var dayA = new Day16(@"D2FE28");
            Assert.AreEqual(6, Convert.ToInt32(dayA.Part1));

            var dayB = new Day16(@"38006F45291200");
            Assert.AreEqual(9, Convert.ToInt32(dayB.Part1));

            var dayC = new Day16(@"EE00D40C823060");
            Assert.AreEqual(14, Convert.ToInt32(dayC.Part1));

            var dayD = new Day16(@"8A004A801A8002F478");
            Assert.AreEqual(16, Convert.ToInt32(dayD.Part1));

            var dayE = new Day16(@"620080001611562C8802118E34");
            Assert.AreEqual(12, Convert.ToInt32(dayE.Part1));

            var dayF = new Day16(@"C0015000016115A2E0802F182340");
            Assert.AreEqual(23, Convert.ToInt32(dayF.Part1));

            var dayG = new Day16(@"A0016C880162017C3686B18A3D4780");
            Assert.AreEqual(31, Convert.ToInt32(dayG.Part1));
        }

        [Test]
        public void SolvePart2_SmallExample_ReturnsCorrectValue()
        {
            var dayA = new Day16(@"C200B40A82");
            Assert.AreEqual(3, Convert.ToInt64(dayA.Part2));

            var dayB = new Day16(@"04005AC33890");
            Assert.AreEqual(54, Convert.ToInt64(dayB.Part2));

            var dayC = new Day16(@"880086C3E88112");
            Assert.AreEqual(7, Convert.ToInt64(dayC.Part2));

            var dayD = new Day16(@"D8005AC2A8F0");
            Assert.AreEqual(1, Convert.ToInt64(dayD.Part2));

            var dayE = new Day16(@"F600BC2D8F");
            Assert.AreEqual(0, Convert.ToInt64(dayE.Part2));

            var dayF = new Day16(@"9C005AC2F8F0");
            Assert.AreEqual(0, Convert.ToInt64(dayF.Part2));

            var dayG = new Day16(@"9C0141080250320F1802104A08");
            Assert.AreEqual(1, Convert.ToInt64(dayG.Part2));

        }
    }
}