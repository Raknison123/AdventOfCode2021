using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Solutions
{
    public class Day04 : DayBase
    {
        List<int> inputNumbers;
        List<List<List<(int value, bool visited)>>> boards;
        string[] splitted;

        public Day04(string input = null) : base(input)
        {
            splitted = InputComplete.Split(new string[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.None);
            inputNumbers = splitted.First().Split(',').Select(int.Parse).ToList();
        }

        protected override object SolvePart1()
        {
            boards = splitted.Skip(1)
                         .Select(b => b.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None)
                                       .Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                                     .Select(x => (value: Convert.ToInt32(x), visited: false)).ToList()).ToList()).ToList();
            foreach (int number in inputNumbers)
            {
                for (int boardNum = 0; boardNum < boards.Count(); boardNum++)
                {
                    for (int row = 0; row < boards[boardNum].Count; row++)
                    {
                        for (int column = 0; column < boards[boardNum][row].Count; column++)
                        {
                            if (boards[boardNum][row][column].value == number)
                            {
                                boards[boardNum][row][column] = (number, true);
                            }

                            if (boards[boardNum][row].All(x => x.visited))
                            {
                                return boards[boardNum].SelectMany(x => x).Where(x => !x.visited).Sum(x => x.value) * number;
                            }
                        }
                    }
                }
            }

            return "no winner";
        }

        protected override object SolvePart2()
        {
            boards = splitted.Skip(1)
                        .Select(b => b.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None)
                                      .Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                                    .Select(x => (value: Convert.ToInt32(x), visited: false)).ToList()).ToList()).ToList();
            foreach (int number in inputNumbers)
            {
                for (int boardNum = 0; boardNum < boards.Count(); boardNum++)
                {
                    for (int row = 0; row < boards[boardNum].Count; row++)
                    {
                        for (int column = 0; column < boards[boardNum][row].Count; column++)
                        {
                            if (boards[boardNum][row][column] == (number, false))
                            {
                                boards[boardNum][row][column] = (number, true);
                            }
                        }
                    }

                    if (boards.All(board =>
                                    {
                                        for (int column = 0; column < board[0].Count; column++)
                                        {
                                            if (Enumerable.Range(0, 5).Select(row => board[row][column]).All(x => x.visited))
                                            {
                                                return true;
                                            }
                                        }

                                        if (board.Any(row => row.All(r => r.visited)))
                                        {
                                            return true;
                                        }

                                        return false;
                                    }))
                    {
                        return boards[boardNum].SelectMany(x => x.Where(x => !x.visited).Select(x => x.value)).Sum() * number;
                    }
                }
            }

            return "no winner";
        }
    }
}
