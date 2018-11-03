﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Cama.Core.Creator.Mutators;
using Cama.Core.Creator.Mutators.BinaryExpressionMutators;
using Cama.Core.Execution.Report;
using Cama.Core.Execution.Report.Cama;
using Cama.Core.Execution.Report.Html;
using Cama.Core.Execution.Report.Markdown;
using Cama.Core.Execution.Report.Trx;
using Cama.Service.Commands.Mutation.CreateMutations;
using Cama.Service.Commands.Mutation.ExecuteMutations;
using Cama.Service.Commands.Project.OpenProject;
using Cama.Service.Commands.Report.Creator;
using MediatR;

namespace Cama.Console
{
    public class ConsoleMutationExecutor
    {
        private readonly IMediator _mediator;

        public ConsoleMutationExecutor(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> ExecuteMutationRunner(string configPath, string savePath)
        {
            var mutators = new List<IMutator>
            {
                new MathMutator(),
                new ConditionalBoundaryMutator(),
                new NegateConditionalMutator(),
                new ReturnValueMutator(),
                new IncrementsMutator(),
                new NegateTypeCompabilityMutator()
            };

            var config = await _mediator.Send(new OpenProjectCommand(configPath));
            var mutationDocuments = await _mediator.Send(new CreateMutationsCommand(config, mutators));
            var results = await _mediator.Send(new ExecuteMutationsCommand(config, mutationDocuments, null));

            await _mediator.Send(new CreateReportCommand(results, new List<ReportCreator>
            {
                new TrxReportCreator(savePath),
                new MarkdownReportCreator(Path.ChangeExtension(savePath, ".md")),
                new CamaReportCreator(Path.ChangeExtension(savePath, ".cama")),
                new HtmlOnlyBodyReportCreator(Path.ChangeExtension(savePath, ".html"))
            }));

            return !results.Any(r => r.Survived);
        }
    }
}
