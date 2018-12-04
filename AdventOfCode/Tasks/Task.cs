using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace AdventOfCode.Tasks
{
    class Task
    {
        public string[] Input { get; set; }
        
        public Task(string file)
        {
            Input = File.ReadAllLines(file);
        }
    }
}
