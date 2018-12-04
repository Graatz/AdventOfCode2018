using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode.Tasks
{
    struct FabricPiece
    {
        public int Id { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }

    // Possible optimization: determining intersections just from the top, left, width and height data (no need to bruteforce)
    class Day3 : Task
    {
        List<FabricPiece> FabricPieces;

        public Day3(string file) : base(file)
        {
            FabricPieces = ParseInput();
        }

        public int Part1()
        {
            var result = 0;
            int[,] fabric = new int[1000, 1000];

            foreach (var piece in FabricPieces)
            {
                for (int i = piece.Left; i < piece.Left + piece.Width; i++)
                {
                    for (int j = piece.Top; j < piece.Top + piece.Height; j++)
                    {
                        if (fabric[i, j] == 0)
                            fabric[i, j] = piece.Id;
                        else
                            result++;
                    }
                }
            }

            return result;
        }

        public int Part2()
        {
            Dictionary<int, bool> intersections = new Dictionary<int, bool>();
            int[,] fabric = new int[1000, 1000];

            foreach (var piece in FabricPieces)
            {
                for (int i = piece.Left; i < piece.Left + piece.Width; i++)
                {
                    for (int j = piece.Top; j < piece.Top + piece.Height; j++)
                    {
                        if (fabric[i, j] == 0)
                        {
                            if (!intersections.ContainsKey(piece.Id))
                                intersections.Add(piece.Id, false);

                            fabric[i, j] = piece.Id;
                        }
                        else if (fabric[i, j] != 0)
                        {
                            intersections[piece.Id] = true;
                            intersections[fabric[i, j]] = true;
                        }
                    }
                }
            }

            return intersections.Where(i => i.Value == false).ToList()[0].Key;
        }

        public List<FabricPiece> ParseInput()
        {
            List<FabricPiece> result = new List<FabricPiece>();
            char[] separators = new char[] { ' ', ',', 'x', '#', ':', '#', '@' };

            foreach (var line in Input)
            {
                string[] values = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                FabricPiece inputLine = new FabricPiece()
                {
                    Id = Int32.Parse(values[0]),
                    Left = Int32.Parse(values[1]),
                    Top = Int32.Parse(values[2]),
                    Width = Int32.Parse(values[3]),
                    Height = Int32.Parse(values[4])
                };

                result.Add(inputLine);
            }

            return result;
        }
    }
}
