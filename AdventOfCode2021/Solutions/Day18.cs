using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Solutions
{
    public class Day18 : DayBase
    {
        public Day18(string input = null) : base(input)
        {

        }

        protected override object SolvePart1()
        {
            return Input.Select(ParseNumber).Aggregate(
                new Number(),
                (acc, number) => !acc.Any() ? number : Sum(acc, number),
                Magnitude
            );
        }

        protected override object SolvePart2()
        {
            var magnitudes = new List<long>();
            var numbers = Input.Select(ParseNumber).ToList();
            for (int i = 0; i < numbers.Count; i++)
            {
                for (int j = 0; j < numbers.Count; j++)
                {
                    if (j == i) continue;
                    magnitudes.Add(Magnitude(Sum(numbers[i], numbers[j])));
                }
            }

            return magnitudes.Max();
        }

        long Magnitude(Number number)
        {
            {
                var iToken = 0;
                long computeRecursive()
                {
                    var token = number[iToken++];
                    if (token.kind == TokenKind.Digit)
                    {
                        return token.value;
                    }
                    else
                    {
                        // take left and right side of the pair
                        var left = computeRecursive();
                        var right = computeRecursive();
                        iToken++; // don't forget to eat the closing parenthesis
                        return 3 * left + 2 * right;
                    }
                }

                return computeRecursive();
            }
        }

        Number Sum(Number numberA, Number numberB) => Reduce(Number.Pair(numberA, numberB));

        Number Reduce(Number number)
        {
            while (Explode(number) || Split(number))
            {

            }

            return number;
        }

        private bool Explode(Number number)
        {
            int level = 0;
            for (int i = 0; i < number.Count; i++)
            {
                if (number[i].kind == TokenKind.Open)
                {
                    level++;
                }
                else if (number[i].kind == TokenKind.Close)
                {
                    level--;
                }

                if (level == 5)
                {
                    var explodingPair = number.Skip(i + 1).Take(2).ToList();

                    //Check left side
                    for (int j = i; j > 0; j--)
                    {
                        if (number[j].kind == TokenKind.Digit)
                        {
                            number[j] = new Token(TokenKind.Digit, number[j].value + explodingPair[0].value);
                            break;
                        }
                    }

                    //Check right side
                    for (int j = i + 3; j < number.Count; j++)
                    {
                        if (number[j].kind == TokenKind.Digit)
                        {
                            number[j] = new Token(TokenKind.Digit, number[j].value + explodingPair[1].value);
                            break;
                        }
                    }

                    number[i] = new Token(TokenKind.Digit, 0);
                    number.RemoveRange(i + 1, 3);
                    return true;
                }
            }

            return false;
        }

        private bool Split(Number number)
        {
            for (int i = 0; i < number.Count; i++)
            {
                var digit = number[i];
                if (digit.kind == TokenKind.Digit && digit.value > 9)
                {
                    var splitIndex = number.IndexOf(digit);
                    var tooHighDigit = number[splitIndex].value;
                    number.RemoveAt(splitIndex);
                    number.InsertRange(
                        splitIndex,
                        new List<Token> { new Token(TokenKind.Open, 0),
                                      new Token(TokenKind.Digit, tooHighDigit / 2),
                                      new Token(TokenKind.Digit, (int)Math.Ceiling(tooHighDigit / 2d)),
                                      new Token(TokenKind.Close, 0), });
                    return true;
                }
            }

            return false;
        }

        Number ParseNumber(string st)
        {
            var res = new Number();
            var n = "";
            foreach (var ch in st)
            {
                if (ch >= '0' && ch <= '9')
                {
                    n += ch;
                }
                else
                {
                    if (n != "")
                    {
                        res.Add(new Token(TokenKind.Digit, int.Parse(n)));
                        n = "";
                    }
                    if (ch == '[')
                    {
                        res.Add(new Token(TokenKind.Open));
                    }
                    else if (ch == ']')
                    {
                        res.Add(new Token(TokenKind.Close));
                    }
                }
            }
            if (n != "")
            {
                res.Add(new Token(TokenKind.Digit, int.Parse(n)));
            }
            return res;
        }
    }

    enum TokenKind
    {
        Open,
        Close,
        Digit
    }

    record Token(TokenKind kind, int value = 0);

    class Number : List<Token>
    {
        public static Number Pair(Number a, Number b)
        {
            var number = new Number();
            number.Add(new Token(TokenKind.Open));
            number.AddRange(a);
            number.AddRange(b);
            number.Add(new Token(TokenKind.Close));
            return number;
        }
    };
}