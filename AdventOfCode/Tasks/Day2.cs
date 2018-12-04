using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Tasks
{
    class Day2 : Task
    {
        public Day2(string file) : base(file)
        {

        }

        public int Part1()
        {
            int twos = 0, threes = 0;

            foreach (var line in Input)
            {
                Dictionary<char, int> counter = new Dictionary<char, int>();

                foreach (var letter in line)
                {
                    if (counter.ContainsKey(letter))
                        counter[letter] += 1;
                    else
                        counter.Add(letter, 1);
                }

                twos += counter.ContainsValue(2) ? 1 : 0;
                threes += counter.ContainsValue(3) ? 1 : 0;
            }

            return twos * threes;
        }

        public string Part2()
        {
            for (int i = 0; i < Input.Length; i++)
            {
                for (int j = 0; j < Input.Length; j++)
                {
                    if (i == j) continue;

                    int counter = 0, lastIndex = 0;

                    for (int k = 0; k < Input[i].Length; k++)
                    {
                        if (!Input[i][k].Equals(Input[j][k]))
                        {
                            if (counter >= 1) break;
                            counter += 1;
                            lastIndex = k;
                        }

                        if ((k == Input[i].Length - 1) && (counter == 1))
                            return Input[i].Remove(lastIndex, 1);
                    }
                }
            }

            return "Not found";
        }
    }
}
