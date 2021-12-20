using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Solutions
{
    public class Day19 : DayBase
    {
        private List<Scanner> matchedScanners;

        public Day19(string input = null) : base(input)
        {

        }

        protected override object SolvePart1()
        {
            var totalBeacons = new HashSet<Coordinate>();
            List<Scanner> scanners = ParseScanners();

            matchedScanners = new List<Scanner>
            {
                scanners[0]
            };

            foreach (var beacon in scanners[0].Beacons)
            {
                totalBeacons.Add(beacon);
            }

            Queue<Scanner> toMatchScanners = new();
            foreach (var scanner in scanners.Skip(1))
            {
                toMatchScanners.Enqueue(scanner);
            }

            while (toMatchScanners.Any())
            {
                var toMatch = toMatchScanners.Dequeue();
                bool isMatch = false;
                foreach (var matched in matchedScanners)
                {
                    (isMatch, Scanner matchedScanner) = TryFindMatchingBeacons(matched, toMatch, out List<Coordinate> resultBeacons);
                    if (isMatch)
                    {
                        matchedScanners.Add(matchedScanner);
                        foreach (var result in resultBeacons)
                        {
                            totalBeacons.Add(result);
                        }
                        break;
                    }
                }
                if (!isMatch)
                {
                    toMatchScanners.Enqueue(toMatch);
                }
            }

            return totalBeacons.Count;
        }


        protected override object SolvePart2()
        {
            var manhattanDistances = new List<long>();
            for (int i = 0; i < matchedScanners.Count; i++)
            {
                for (int j = 0; j < matchedScanners.Count; j++)
                {
                    if (j == i)
                    {
                        continue;
                    }

                    manhattanDistances.Add(CalculateManhattanDistance(matchedScanners[i], matchedScanners[j]));
                }
            }

            return manhattanDistances.Max();
        }

        private long CalculateManhattanDistance(Scanner scanner1, Scanner scanner2)
        {
            return Math.Abs(scanner1.Position.X - scanner2.Position.X) + Math.Abs(scanner1.Position.Y - scanner2.Position.Y) + Math.Abs(scanner1.Position.Z - scanner2.Position.Z);
        }

        private (bool isMatch, Scanner matchedScanner) TryFindMatchingBeacons(Scanner normalizedScanner, Scanner scannerToTranslate, out List<Coordinate> resultBeacons)
        {
            resultBeacons = new List<Coordinate>();
            for (int x = 0; x < 360; x += 90)
            {
                for (int y = 0; y < 360; y += 90)
                {
                    for (int z = 0; z < 360; z += 90)
                    {
                        double angleX = Math.PI * x / 180.0;
                        double angleY = Math.PI * y / 180.0;
                        double angleZ = Math.PI * z / 180.0;

                        var zMatrix = new double[3, 3] { { Math.Cos(angleZ), -Math.Sin(angleZ), 0 },
                                                      { Math.Sin(angleZ), Math.Cos(angleZ), 0 },
                                                      { 0, 0, 1 }};
                        var yMatrix = new double[3, 3] { { Math.Cos(angleY), 0, -Math.Sin(angleY) },
                                                      { 0, 1, 0 },
                                                      { Math.Sin(angleY), 0, Math.Cos(angleY) }};
                        var xMatrix = new double[3, 3] { { 1, 0, 0 },
                                                      { 0, Math.Cos(angleX), -Math.Sin(angleX) },
                                                      { 0, Math.Sin(angleX), Math.Cos(angleX) }};

                        var product = Matrix.Multiply(Matrix.Multiply(zMatrix, yMatrix), xMatrix);

                        foreach (var normalizedBeacon in normalizedScanner.Beacons)
                        {
                            foreach (var toTranslateBeacon in scannerToTranslate.Beacons)
                            {
                                var translatedScanner = scannerToTranslate.Translate(toTranslateBeacon, normalizedBeacon, product);
                                var intersect = normalizedScanner.Beacons.Intersect(translatedScanner.Beacons);
                                if (intersect.Count() >= 12)
                                {
                                    resultBeacons.AddRange(translatedScanner.Beacons);
                                    return (true, translatedScanner);
                                }
                            }
                        }
                    }
                }
            }

            return (false, null);
        }

        private List<Scanner> ParseScanners()
        {
            var scanners = new List<Scanner>();
            Scanner scanner = null;
            foreach (var row in Input)
            {
                if (row.StartsWith("---"))
                {
                    scanner = new Scanner(Convert.ToInt32(row.Split("--- scanner ")[1].Split(" ")[0]));
                    scanner.Position = new Coordinate(0, 0, 0);
                    scanners.Add(scanner);

                }
                else if (!string.IsNullOrEmpty(row))
                {
                    var splitted = row.Split(",");
                    var x = Convert.ToInt32(splitted[0]);
                    var y = Convert.ToInt32(splitted[1]);
                    var z = Convert.ToInt32(splitted[2]);
                    scanner.Beacons.Add(new Coordinate(x, y, z));
                }
            }

            return scanners;
        }
    }

    record class Coordinate(int X, int Y, int Z);

    class Scanner
    {
        public Scanner(int id)
        {
            Id = id;
            Beacons = new List<Coordinate>();
        }

        public int Id { get; }

        public Coordinate Position { get; set; }

        public List<Coordinate> Beacons { get; set; }

        public Scanner Translate(Coordinate targetBeacon, Coordinate normalizedBeacon, double[,] rotationMatrix)
        {
            var scanner = new Scanner(Id);
            var transformedBeacon = GetTransformedValue(targetBeacon, rotationMatrix);
            var shift = new Coordinate(normalizedBeacon.X - transformedBeacon.X,
                                       normalizedBeacon.Y - transformedBeacon.Y,
                                       normalizedBeacon.Z - transformedBeacon.Z);
            scanner.Position = shift;

            foreach (var beacon in Beacons)
            {
                var trans = GetTransformedValue(beacon, rotationMatrix);
                scanner.Beacons.Add(new Coordinate(trans.X + shift.X, trans.Y + shift.Y, trans.Z + shift.Z));
            }

            return scanner;
        }

        private Coordinate GetTransformedValue(Coordinate targetBeacon, double[,] orientationMatrix)
        {
            var coordinate = new double[3, 1] { { targetBeacon.X }, { targetBeacon.Y }, { targetBeacon.Z } };
            var transformed = Matrix.Multiply(orientationMatrix, coordinate);

            return new Coordinate(Convert.ToInt32(transformed[0, 0]), Convert.ToInt32(transformed[1, 0]), Convert.ToInt32(transformed[2, 0]));
        }
    }

    static class Matrix
    {
        public static double[,] Multiply(double[,] matrix1, double[,] matrix2)
        {
            // cahing matrix lengths for better performance  
            var matrix1Rows = matrix1.GetLength(0);
            var matrix1Cols = matrix1.GetLength(1);
            var matrix2Rows = matrix2.GetLength(0);
            var matrix2Cols = matrix2.GetLength(1);

            // checking if product is defined  
            if (matrix1Cols != matrix2Rows)
                throw new InvalidOperationException
                  ("Product is undefined. n columns of first matrix must equal to n rows of second matrix");

            // creating the final product matrix  
            double[,] product = new double[matrix1Rows, matrix2Cols];

            // looping through matrix 1 rows  
            for (int matrix1_row = 0; matrix1_row < matrix1Rows; matrix1_row++)
            {
                // for each matrix 1 row, loop through matrix 2 columns  
                for (int matrix2_col = 0; matrix2_col < matrix2Cols; matrix2_col++)
                {
                    // loop through matrix 1 columns to calculate the dot product  
                    for (int matrix1_col = 0; matrix1_col < matrix1Cols; matrix1_col++)
                    {
                        product[matrix1_row, matrix2_col] +=
                          matrix1[matrix1_row, matrix1_col] *
                          matrix2[matrix1_col, matrix2_col];
                    }
                }
            }

            return product;
        }
    }
}