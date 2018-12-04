using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace AdventOfCode.Tasks
{
    class Day1 : Task
    {
        public Day1(string file) : base(file)
        {
            Input = File.ReadAllLines(file);
        }

        public int Part1()
        {
            var result = 0;

            foreach (var line in Input)
                result += Int32.Parse(line);

            return result;
        }

        public int Part2()
        {
            HashSet<int> frequenciesSeen = new HashSet<int>();
            var result = 0;

            while (true)
            {
                foreach (var line in Input)
                {
                    result += Int32.Parse(line);

                    if (frequenciesSeen.Contains(result))
                        return result;

                    frequenciesSeen.Add(result);
                }
            };

            return -1;
        }
    }
}
