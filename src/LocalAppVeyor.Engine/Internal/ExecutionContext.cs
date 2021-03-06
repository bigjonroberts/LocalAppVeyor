﻿using System;
using LocalAppVeyor.Engine.Configuration;

namespace LocalAppVeyor.Engine.Internal
{
    internal class ExecutionContext
    {
        public MatrixJob CurrentJob { get; }
        
        public string RepositoryDirectory { get; }

        public ExpandableString CloneDirectory { get; }

        public BuildConfiguration BuildConfiguration { get; }

        public IPipelineOutputter Outputter { get; }

        public ExecutionContext(
            MatrixJob currentJob,
            BuildConfiguration buildConfiguration,
            IPipelineOutputter outputter,
            string repositoryDirectory,
            ExpandableString cloneDirectory)
        {
            if (currentJob == null) throw new ArgumentNullException(nameof(currentJob));
            if (buildConfiguration == null) throw new ArgumentNullException(nameof(buildConfiguration));
            if (outputter == null) throw new ArgumentNullException(nameof(outputter));
            if (repositoryDirectory == null) throw new ArgumentNullException(nameof(repositoryDirectory));

            CurrentJob = currentJob;
            BuildConfiguration = buildConfiguration;
            Outputter = outputter;
            RepositoryDirectory = repositoryDirectory;
            CloneDirectory = cloneDirectory;
        }
    }
}
