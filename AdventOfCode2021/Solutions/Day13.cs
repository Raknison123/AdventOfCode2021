using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AdventOfCode2021.Solutions
{
    public class Day13 : DayBase
    {
        private readonly HashSet<(int x, int y)> pixels = new();
        private readonly List<(string axis, int coord)> foldings = new();

        public Day13(string input = null) : base(input)
        {
            var inputParts = InputComplete.Split(new string[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
            var inputFirstPart = inputParts[0].Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            var inputSecondPart = inputParts[1].Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < inputFirstPart.Length; i++)
            {
                var splitted = inputFirstPart[i].ToString().Split(',');
                pixels.Add((Convert.ToInt32(splitted[0].ToString()), Convert.ToInt32(splitted[1].ToString())));
            }

            for (int i = 0; i < inputSecondPart.Length; i++)
            {
                var splitted = inputSecondPart[i].ToString().Split('=');
                foldings.Add((splitted[0].Last().ToString(), Convert.ToInt32(splitted[1])));
            }
        }

        protected override object SolvePart1()
        {
            HashSet<(int x, int y)> foldedPixels = null;
            foreach (var fold in foldings.Take(1))
            {
                foldedPixels = Fold(foldedPixels ?? pixels, fold);
            }

            return foldedPixels.Count;
        }

        protected override object SolvePart2()
        {
            HashSet<(int x, int y)> foldedPixels = null;
            foreach (var fold in foldings)
            {
                foldedPixels = Fold(foldedPixels ?? pixels, fold);
            }

            DrawCodeImage(foldedPixels);
            return "see Code.jpg";
        }

        private static HashSet<(int x, int y)> Fold(HashSet<(int x, int y)> pixels, (string axis, int coord) fold)
        {
            HashSet<(int x, int y)> outputPixels = new();

            foreach (var pixel in pixels)
            {
                var needsFolding = fold.axis == "y" ? pixel.y > fold.coord : pixel.x > fold.coord;
                if (needsFolding)
                {
                    int newXCoord = fold.axis == "x" ? pixel.x - ((pixel.x - fold.coord) * 2) : pixel.x;
                    int newYCoord = fold.axis == "y" ? pixel.y - ((pixel.y - fold.coord) * 2) : pixel.y;
                    outputPixels.Add((newXCoord, newYCoord));
                }
                else
                {
                    outputPixels.Add((pixel.x, pixel.y));
                }
            }

            return outputPixels;
        }

        private static void DrawCodeImage(HashSet<(int x, int y)> foldedPixels)
        {
            Bitmap myBitmap = new Bitmap(foldedPixels.Max(p => p.x) + 1, foldedPixels.Max(p => p.y) + 1);
            foreach (var pixel in foldedPixels)
            {
                myBitmap.SetPixel(pixel.x, pixel.y, Color.Black);
            }

            myBitmap.Save("Code.jpg");
        }
    }
}
