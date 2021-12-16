using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Solutions
{
    public class Day16 : DayBase
    {
        private string inputBinary;

        public Day16(string input = null) : base(input)
        {
            var packetVersions = new List<long>();
            this.inputBinary = string.Empty;
            // Start with main packet
            foreach (var hex in InputComplete.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)[0])
            {
                var bitGroup = Convert.ToString(Convert.ToInt64(hex.ToString(), 16), 2);
                inputBinary += bitGroup.PadLeft(4, '0');
            }
        }

        protected override object SolvePart1()
        {
            var packetVersions = new List<long>();
            ParsePacket(packetVersions, inputBinary);
            return packetVersions.Sum();
        }

        protected override object SolvePart2()
        {
            (long result, _) = ParsePacket(new List<long>(), inputBinary);
            return result;
        }

        private static (long value, int parsedLength) ParsePacket(List<long> packetVersions, string inputBinary)
        {
            var packetVersion = Convert.ToInt32(inputBinary.Substring(0, 3), 2);
            packetVersions.Add(packetVersion);
            var typeId = Convert.ToInt32(inputBinary.Substring(3, 3), 2);

            // literal packet
            if (typeId == 4)
            {
                int parsedLengthCounter = 6;
                int groupNumber = 0;
                var groupBits = string.Empty;
                while (inputBinary[6 + (groupNumber * 5)] == '1')
                {
                    parsedLengthCounter += 5;
                    groupBits += inputBinary.Substring(7 + (groupNumber * 5), 4);
                    groupNumber++;
                }

                parsedLengthCounter += 5;
                groupBits += inputBinary.Substring(7 + (groupNumber * 5), 4);
                long value = Convert.ToInt64(groupBits, 2);
                return (value, parsedLengthCounter);
            }

            // operator packet
            else
            {
                int parsedLength = 0;
                char lengthTypeId = inputBinary[6];
                List<long> results = new();
                if (lengthTypeId == '0')
                {
                    var subPacketLength = Convert.ToInt32(inputBinary.Substring(7, 15), 2);
                    var subPackets = inputBinary.Substring(22, subPacketLength);

                    while (parsedLength < subPacketLength)
                    {
                        (long value, int parsed) = ParsePacket(packetVersions, subPackets.Substring(parsedLength, subPackets.Length - parsedLength));
                        parsedLength += parsed;
                        results.Add(value);
                    }

                    return (CalculateResult(results, typeId), parsedLength + 22);
                }
                else
                {
                    var numberOfSubPackets = Convert.ToInt32(inputBinary.Substring(7, 11), 2);
                    var startIndex = 18;

                    for (int i = 0; i < numberOfSubPackets; i++)
                    {
                        (long value, int parsed) = ParsePacket(packetVersions, inputBinary.Substring(startIndex + parsedLength, inputBinary.Length - startIndex - parsedLength));
                        parsedLength += parsed;
                        results.Add(value);
                    }

                    return (CalculateResult(results, typeId), parsedLength + 18);

                }
            }
        }

        private static long CalculateResult(List<long> results, int typeId)
        {
            return typeId switch
            {
                0 => results.Sum(),
                1 => results.Aggregate((a, x) => a * x),
                2 => results.Min(),
                3 => results.Max(),
                5 => results.First() > results.Last() ? 1 : 0,
                6 => results.First() < results.Last() ? 1 : 0,
                7 => results.First() == results.Last() ? 1 : 0,
                _ => 0,
            };
        }
    }
}