using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Tasks
{
    class Day5 : Task
    {
        public Day5(string file) : base(file)
        {

        }

        public int Part1()
        {
            var data = Input[0];
            return PolymerReaction(data);
        }

        public int Part2()
        {
            var min = Input[0].Length;
            for (char i = 'A'; i <= 'Z'; i++)
            {
                string newPolymer = RemoveUnitsOfType(new char[] { (char)i, (char)char.ToLower(i) });
                var reactedPolymerLength = PolymerReaction(newPolymer);

                if (reactedPolymerLength < min)
                    min = reactedPolymerLength;
            }
            return min;
        }

        public int PolymerReaction(string polymer)
        {
            for (int i = 0; i < polymer.Length; i++)
            {
                if (i == 0 && Math.Abs(polymer[i] - polymer[i + 1]) == 32)
                {
                    polymer = polymer.Remove(i, 2);
                    i = -1;
                }
                else if (i != 0 && Math.Abs(polymer[i] - polymer[i - 1]) == 32)
                {
                    polymer = polymer.Remove(i - 1, 2);
                    i = -1;
                }
                else if (i != polymer.Length - 1 && Math.Abs(polymer[i] - polymer[i + 1]) == 32)
                {
                    polymer = polymer.Remove(i, 2);
                    i = -1;
                }
            }

            return polymer.Length;
        }

        public string RemoveUnitsOfType(char[] types)
        {
            var data = Input[0];

            for (int i = 0; i < data.Length; i++)
            {
                if ((int)data[i] == (int)types[0] || (int)data[i] == (int)types[1])
                {
                    data = data.Remove(i, 1);
                    i--;
                }
            }

            return data;
        }
    }
}
