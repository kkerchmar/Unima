﻿using Cama.Infrastructure.Tabs;
using Cama.Module.Start.Sections.NewProject;

namespace Cama.Module.Start.Tab
{
    public class StartModuleTabOpener : IStartModuleTabOpener
    {
        private readonly IMainTabContainer _mainTabContainer;

        public StartModuleTabOpener(IMainTabContainer mainTabContainer)
        {
            _mainTabContainer = mainTabContainer;
        }

        public void OpenNewProjectTab()
        {
            _mainTabContainer.AddTab(new NewProjectDialog());
        }
    }
}