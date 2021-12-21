using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AdventOfCode2021.Solutions
{
    public class Day20 : DayBase
    {

        public Day20(string input = null) : base(input)
        {

        }

        protected override object SolvePart1()
        {
            var splitted = InputComplete.Split(new string[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.None);
            var algorithm = splitted[0].Replace("\r\n", "").Select(s => s == '#' ? 1 : 0).ToList();
            Dictionary<(int x, int y), int> image = ParseInput(splitted);


            for (int i = 0; i < 2; i++)
            {
                image = EnhanceImage(image, algorithm);
                image = ExtendImage(image);

            }

            DrawCodeImage(image);
            return image.Count(i => i.Value == 1);
        }

        protected override object SolvePart2()
        {
            var splitted = InputComplete.Split(new string[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.None);
            var algorithm = splitted[0].Replace("\r\n", "").Select(s => s == '#' ? 1 : 0).ToList();
            Dictionary<(int x, int y), int> image = ParseInput(splitted);


            for (int i = 0; i < 50; i++)
            {
                image = EnhanceImage(image, algorithm);
                image = ExtendImage(image);

            }

            DrawCodeImage(image);
            return image.Count(i => i.Value == 1);
        }

        private Dictionary<(int x, int y), int> ExtendImage(Dictionary<(int x, int y), int> image)
        {
            var firstValue = image.First().Value;
            var minY = image.Min(i => i.Key.y);
            var maxY = image.Max(i => i.Key.y);
            var minX = image.Min(i => i.Key.x);
            var maxX = image.Max(i => i.Key.x);

            for (int y = minY - 3; y <= maxY + 3; y++)
            {
                for (int x = minX - 3; x <= maxX + 3; x++)
                {
                    image.TryAdd((x, y), firstValue);
                }
            }

            return image;
        }

        private Dictionary<(int x, int y), int> EnhanceImage(Dictionary<(int x, int y), int> image, IList<int> algorithm)
        {
            var resultImage = new Dictionary<(int x, int y), int>();

            var minY = image.Min(i => i.Key.y);
            var maxY = image.Max(i => i.Key.y);
            var minX = image.Min(i => i.Key.x);
            var maxX = image.Max(i => i.Key.x);

            for (int y = minY + 1; y < maxY; y++)
            {
                for (int x = minX + 1; x < maxX; x++)
                {
                    var outputPixel = EnhancePixel((x, y), image, 3, algorithm);
                    resultImage.Add((x, y), outputPixel);
                }
            }

            return resultImage;
        }

        private int EnhancePixel((int x, int y) pixelPos, Dictionary<(int x, int y), int> image, int windowSize, IList<int> algorithm)
        {
            string calc = string.Empty;
            for (int y = -windowSize / 2; y <= windowSize / 2; y++)
            {
                for (int x = -windowSize / 2; x <= windowSize / 2; x++)
                {
                    if (image.TryGetValue((pixelPos.x + x, pixelPos.y + y), out int value))
                    {
                        calc += value.ToString();
                    }
                }
            }

            var index = Convert.ToInt32(calc, 2);
            return algorithm[index];
        }

        private Dictionary<(int x, int y), int> ParseInput(string[] splitted)
        {
            var pixels = splitted[1].Split(new string[] { "\n\n", "\r\n", "\n" }, StringSplitOptions.None);
            var image = new Dictionary<(int x, int y), int>();

            for (int y = 0; y < pixels.Length; y++)
            {
                for (int x = 0; x < pixels[y].Length; x++)
                {
                    image.Add((x, y), pixels[y][x] == '#' ? 1 : 0);
                }
            }

            var minY = image.Min(i => i.Key.y);
            var minX = image.Min(i => i.Key.x);
            var maxY = image.Max(i => i.Key.y);
            var maxX = image.Max(i => i.Key.x);

            for (int y = minY - 3; y <= maxY + 3; y++)
            {
                for (int x = minX - 3; x <= maxX + 3; x++)
                {
                    image.TryAdd((x, y), 0);
                }
            }

            return image;
        }

        private static void DrawCodeImage(Dictionary<(int x, int y), int> image)
        {
            var width = image.Max(p => p.Key.x) - image.Min(p => p.Key.x) + 1;
            var height = image.Max(p => p.Key.y) - image.Min(p => p.Key.y) + 1;
            var xOffset = Math.Abs(image.Min(p => p.Key.x));
            var yOffset = Math.Abs(image.Min(p => p.Key.y));
            Bitmap myBitmap = new Bitmap(width, height);
            foreach (var pixel in image.Where(i => i.Value == 1))
            {
                myBitmap.SetPixel(pixel.Key.x + xOffset, pixel.Key.y + yOffset, Color.Black);
            }

            myBitmap.Save($"image.jpg");
        }
    }
}