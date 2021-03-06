﻿using System;

namespace Unima.Core.Loggers
{
    public class MutationRunLoggerFactory
    {
        public IMutationRunLogger GetMutationRunLogger(MutationRunLogger mutationRunLogger)
        {
            switch (mutationRunLogger)
            {
                case MutationRunLogger.Azure:
                    return new AzureMutationRunLogger();
                default:
                    throw new ArgumentOutOfRangeException(nameof(mutationRunLogger), mutationRunLogger, null);
            }
        }
    }
}
