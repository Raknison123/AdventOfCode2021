using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Solutions
{
    public class Day03 : DayBase
    {
        private List<string> inputBits;

        public Day03(string input = null) : base(input)
        {
            inputBits = Input.ToList();
        }

        protected override object SolvePart1()
        {
            string gammaBits = string.Empty;
            string epsilonBits = string.Empty;

            for (int i = 0; i < inputBits.First().Length; i++)
            {
                int numOfOnes = 0;
                int numOfZeros = 0;
                for (int j = 0; j < inputBits.Count; j++)
                {
                    _ = Convert.ToInt32(inputBits[j][i].ToString()) == 0 ? numOfZeros++ : numOfOnes++;
                }
                gammaBits += numOfOnes > numOfZeros ? "1" : "0";
                epsilonBits += numOfOnes > numOfZeros ? "0" : "1";
            }

            int gamma = Convert.ToInt32(gammaBits, 2);
            int epsilon = Convert.ToInt32(epsilonBits, 2);

            return gamma * epsilon;
        }

        protected override object SolvePart2()
        {
            var oxygenGenerRating = CalculateRating(Input.ToList(), false);
            var co2ScrubberGenerRating = CalculateRating(Input.ToList(), true);
            return oxygenGenerRating * co2ScrubberGenerRating;
        }

        private int CalculateRating(List<string> inputBits, bool inverse)
        {
            int numberOfColumns = inputBits.First().Length;

            for (int i = 0; i < numberOfColumns; i++)
            {
                int numOfOnes = 0;
                int numOfZeros = 0;

                for (int j = 0; j < inputBits.Count; j++)
                {
                    _ = Convert.ToInt32(inputBits[j][i].ToString()) == 0 ? numOfZeros++ : numOfOnes++;
                }

                string bitToChoose = string.Empty;
                if (inverse)
                {
                    bitToChoose = numOfZeros <= numOfOnes ? "0" : "1";
                }
                else
                {
                    bitToChoose = numOfOnes >= numOfZeros ? "1" : "0";
                }

                inputBits = inputBits.Where(bit => bit[i].ToString() == bitToChoose).ToList();
                if (inputBits.Count == 1)
                {
                    break;
                }
            }

            return Convert.ToInt32(inputBits[0], 2);
        }
    }
}
