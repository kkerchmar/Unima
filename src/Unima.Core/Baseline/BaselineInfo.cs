﻿using System;

namespace Unima.Core.Baseline
{
    public class BaselineInfo
    {
        public BaselineInfo(string name, TimeSpan executionTime)
        {
            TestProjectName = name;
            ExecutionTime = executionTime;
        }

        public string TestProjectName { get; set; }

        public TimeSpan ExecutionTime { get; set; }

        public TimeSpan GetTestProjectTimeout()
        {
            return ExecutionTime.Add(TimeSpan.FromSeconds(60));
        }
    }
}
