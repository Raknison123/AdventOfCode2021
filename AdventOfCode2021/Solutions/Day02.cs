using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Solutions
{
    public class Day02 : DayBase
    {
        List<(Direction direction, int steps)> commands;

        public Day02(string input = null) : base(input)
        {
            commands = Input.Select(x =>
            {
                var splitted = x.Split(' ');
                Enum.TryParse(splitted[0], true, out Direction direction);
                return (direction: direction, steps: Convert.ToInt32(splitted[1]));

            }).ToList();
        }

        protected override object SolvePart1()
        {
            (int horizontal, int depth) position = (0, 0);
            foreach (var (direction, steps) in commands)
            {
                switch (direction)
                {
                    case Direction.Forward:
                        position.horizontal += steps;
                        break;
                    case Direction.Down:
                        position.depth += steps;
                        break;
                    case Direction.Up:
                        position.depth -= steps;
                        break;
                    default:
                        break;
                }
            }

            return position.depth * position.horizontal;
        }

        protected override object SolvePart2()
        {
            (int horizontal, int depth, int aim) position = (0, 0, 0);
            foreach (var (direction, steps) in commands)
            {
                switch (direction)
                {
                    case Direction.Forward:
                        position.horizontal += steps;
                        position.depth += position.aim * steps;
                        break;
                    case Direction.Down:
                        position.aim += steps;
                        break;
                    case Direction.Up:
                        position.aim -= steps;
                        break;
                    default:
                        break;
                }
            }

            return position.depth * position.horizontal;
        }

        private enum Direction
        {
            Forward,
            Down,
            Up,
        }
    }
}
