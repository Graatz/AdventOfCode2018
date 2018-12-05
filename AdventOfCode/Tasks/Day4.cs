using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace AdventOfCode.Tasks
{
    class Day4 : Task
    {
        public List<GuardAction> AllActions { get; set; }
        public Dictionary<int, Guard> Guards { get; set; }

        public Day4(string file) : base(file)
        {
            AllActions = SetActions();
            Guards = SetGuards();
        }

        public int Part1()
        {
            KeyValuePair<int, Guard> sleepyGuard = Guards
                .OrderByDescending(g => g.Value.MinutesAsleep)
                .First();

            var mostAsleepMinute = sleepyGuard
                .Value
                .SleepyMinutesMap
                .OrderByDescending(m => m.Value)
                .First().Key;

            var sleepyGuardId = sleepyGuard.Key;

            return mostAsleepMinute * sleepyGuardId;
        }

        public int Part2()
        {
            int maxMinute = 0;
            int mostAsleepMinute = 0;
            int guardId = 0;

            foreach (var guard in Guards)
            {
                foreach (var minute in guard.Value.SleepyMinutesMap)
                {
                    if (minute.Value > maxMinute)
                    {
                        maxMinute = minute.Value;
                        mostAsleepMinute = minute.Key;
                        guardId = guard.Key;
                    }
                }
            }

            return mostAsleepMinute * guardId;
        }

        public Dictionary<int, Guard> SetGuards()
        {
            Dictionary<int, Guard> guards = new Dictionary<int, Guard>();
            int currentGuardId = 0;
            int fallsAsleepMinute = 0;

            foreach (var action in AllActions)
            {
                if (action.Value.Contains("Guard"))
                {
                    string[] parsedValue = action.Value.Split
                    (
                        new char[] { ' ', '#' },
                        StringSplitOptions.RemoveEmptyEntries
                    );

                    currentGuardId = int.Parse(parsedValue[1]);

                    if (!guards.ContainsKey(currentGuardId))
                        guards.Add(currentGuardId, new Guard());
                }
                else if (action.Value.Equals("falls asleep"))
                {
                    fallsAsleepMinute = action.Date.Minute;
                }
                else if (action.Value.Equals("wakes up"))
                {
                    for (int i = fallsAsleepMinute; i < action.Date.Minute; i++)
                    {
                        if (!guards[currentGuardId].SleepyMinutesMap.ContainsKey(i))
                            guards[currentGuardId].SleepyMinutesMap.Add(i, 0);

                        guards[currentGuardId].SleepyMinutesMap[i] += 1;
                        guards[currentGuardId].MinutesAsleep += 1;
                    }
                }
            }

            return guards;
        }

        public List<GuardAction> SetActions()
        {
            List<GuardAction> guardActions = new List<GuardAction>();

            char[] separators = new char[] { '[', ']' };
            foreach (var line in Input)
            {
                var parsedLine = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                GuardAction guardAction = new GuardAction()
                {
                    Date = DateTime.Parse(parsedLine[0]),
                    Value = parsedLine[1].Remove(0, 1)
                };

                guardActions.Add(guardAction);
            }

            return guardActions.OrderBy(a => a.Date).ToList();
        }
    }

    class GuardAction
    {
        public DateTime Date { get; set; }
        public string Value { get; set; }
    }

    class Guard
    {
        public Guard()
        {
            Actions = new List<GuardAction>();
            SleepyMinutesMap = new Dictionary<int, int>();
        }

        public List<GuardAction> Actions { get; set; }
        public Dictionary<int, int> SleepyMinutesMap { get; set; }
        public int MinutesAsleep { get; set; }
    }
}