using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode.Tasks
{
    class Coordinate
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public Coordinate(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }

    class Day6 : Task
    {
        public int[,] Grid { get; set; }
        List<Coordinate> Coordinates { get; set; }
        HashSet<int> Infinites { get; set; }

        public Day6(string file) : base(file)
        {
            Infinites = new HashSet<int>();
            Coordinates = new List<Coordinate>();

            ParseInput();

            var rows = Coordinates.OrderByDescending(c => c.Row).First().Row + 1;
            var columns = Coordinates.OrderByDescending(c => c.Column).First().Column + 1;

            Grid = new int[rows, columns];

            SetGrid();
            //PrintGrid();
        }

        public void ParseInput()
        {
            for (int i = 0; i < Input.Length; i++)
            {
                var parsed = Input[i].Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                Coordinate coordinate = new Coordinate(int.Parse(parsed[1]), int.Parse(parsed[0]));
                Coordinates.Add(coordinate);
            }
        }

        public void SetGrid()
        {
            for (int i = 0; i < Grid.GetLength(0); i++)
            {
                for (int j = 0; j < Grid.GetLength(1); j++)
                {
                    var minDistance = Int32.MaxValue;
                    var chosen = 0;

                    for (int k = 0; k < Coordinates.Count; k++)
                    {
                        var distance = CalculateDistance(i, j, Coordinates[k].Row, Coordinates[k].Column);
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            chosen = k + 1;
                        }
                        else if (minDistance == distance)
                            chosen = -1;
                    }

                    if (i == Grid.GetLength(0) - 1 || j == Grid.GetLength(1) - 1 || i == 0 || j == 0)
                        Infinites.Add(chosen);

                    Grid[i, j] = chosen;
                }
            }
        }

        public int CalculateDistance(int row1, int col1, int row2, int col2)
        {
            int distance = Math.Abs(row1 - row2) + Math.Abs(col1 - col2);
            return distance;
        }

        public void PrintGrid()
        {
            for (int i = 0; i < Grid.GetLength(0); i++)
            {
                for (int j = 0; j < Grid.GetLength(1); j++)
                {
                    Console.Write(Grid[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        public int Part1()
        {
            Dictionary<int, int> AreaSizes = new Dictionary<int, int>();

            for (int i = 0; i < Grid.GetLength(0); i++)
            {
                for (int j = 0; j < Grid.GetLength(1); j++)
                {
                    if (Infinites.Contains(Grid[i, j]))
                        continue;

                    if (!AreaSizes.ContainsKey(Grid[i, j]))
                        AreaSizes.Add(Grid[i, j], 0);

                    AreaSizes[Grid[i, j]] += 1;
                }
            }

            return AreaSizes.OrderByDescending(a => a.Value).First().Value;
        }

        public int Part2()
        {
            var result = 0;

            for (int i = 0; i < Grid.GetLength(0); i++)
            {
                for (int j = 0; j < Grid.GetLength(1); j++)
                {
                    var distance = 0;
                    for (int k = 0; k < Coordinates.Count; k++)
                    {
                        distance += CalculateDistance(i, j, Coordinates[k].Row, Coordinates[k].Column);
                    }
                    if (distance < 10000) result += 1;
                }
            }

            return result;
        }
    }
}
