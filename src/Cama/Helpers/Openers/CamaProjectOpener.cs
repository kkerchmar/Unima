﻿using System.IO;
using System.Threading.Tasks;
using Cama.Helpers.Displayers;
using Cama.Helpers.Openers.Tabs;
using Cama.Service.Commands;
using Cama.Service.Commands.Project.OpenProject;
using Cama.Service.Exceptions;
using FluentValidation;

namespace Cama.Helpers.Openers
{
    public class CamaProjectOpener
    {
        private readonly ILoadingDisplayer _loadingDisplayer;
        private readonly IMutationModuleTabOpener _mutationModuleTabOpener;
        private readonly ICommandDispatcher _commandDispatcher;

        public CamaProjectOpener(
            ILoadingDisplayer loadingDisplayer,
            IMutationModuleTabOpener mutationModuleTabOpener,
            ICommandDispatcher commandDispatcher)
        {
            _loadingDisplayer = loadingDisplayer;
            _mutationModuleTabOpener = mutationModuleTabOpener;
            _commandDispatcher = commandDispatcher;
        }

        public async void OpenProject(string path)
        {
            _loadingDisplayer.ShowLoading($"Opening project at {Path.GetFileName(path)}");
            try
            {
                var config = await Task.Run(() => _commandDispatcher.ExecuteCommandAsync(new OpenProjectCommand(path)));
                _mutationModuleTabOpener.OpenOverviewTab(config);
            }
            catch (ValidationException ex)
            {
                ErrorDialogDisplayer.ShowErrorDialog("Unexpected error", "Failed to open project.", ex.Message);
            }
            catch (OpenProjectException ex)
            {
                ErrorDialogDisplayer.ShowErrorDialog("Unexpected error", "Failed to open project.", ex.InnerException?.ToString());
            }
            finally
            {
                _loadingDisplayer.HideLoading();
            }
        }
    }
}