﻿using System.Collections.Generic;
using Cama.Core.Baseline;
using Cama.Core.Solution;

namespace Cama.Core
{
    public class CamaConfig
    {
        public CamaConfig()
        {
            MutationProjects = new List<SolutionProjectInfo>();
            TestProjects = new List<SolutionProjectInfo>();
            Filter = new List<string>();
            IgnoredFiles = new List<string>();
            MaxTestTimeMin = 5;
            TestRunner = "nunit";
            BaselineInfos = new List<BaselineInfo>();
        }

        public string SolutionPath { get; set; }

        public IList<SolutionProjectInfo> MutationProjects { get; set; }

        public IList<SolutionProjectInfo> TestProjects { get; set; }

        public IList<string> Filter { get; set; }

        public IList<string> IgnoredFiles { get; set; }

        public int NumberOfTestRunInstances { get; set; }

        public string BuildConfiguration { get; set; }

        public int MaxTestTimeMin { get; set; }

        public string TestRunner { get; set; }

        public IList<BaselineInfo> BaselineInfos { get; set; }
    }
}
