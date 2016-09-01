﻿using System.IO;
using LocalAppVeyor.Configuration.Model;

namespace LocalAppVeyor.Pipeline.Steps.AppVeyor
{
    public class CloneFolderStep : Step
    {
        private readonly string cloneFolder;

        public CloneFolderStep(BuildConfiguration buildConfiguration) 
            : base(buildConfiguration)
        {
            cloneFolder = buildConfiguration.CloneFolder;
        }

        public override bool Execute(ExecutionContext executionContext)
        {
            if (string.IsNullOrEmpty(cloneFolder))
            {
                return true;
            }

            executionContext.Outputter.Write($"Cloning '{executionContext.WorkingDirectory}' into '{cloneFolder}'...");
            Clone(executionContext.WorkingDirectory, cloneFolder);
            executionContext.Outputter.Write($"Cloning finished.");

            Directory.SetCurrentDirectory(cloneFolder);
            executionContext.Outputter.Write($"Working directory changed to be cloning directory.");

            return true;
        }

        private static void Clone(string source, string destination)
        {
            var dirSource = new DirectoryInfo(source);
            var dirDestination = new DirectoryInfo(destination);

            // empty destination
            foreach (var fileInfo in dirDestination.GetFiles())
            {
                fileInfo.Delete();
            }

            foreach (var directoryInfo in dirDestination.GetDirectories())
            {
                directoryInfo.Delete(true);
            }

            CopyAll(dirSource, dirDestination);
        }

        private static void CopyAll(DirectoryInfo source, DirectoryInfo destination)
        {
            Directory.CreateDirectory(destination.FullName);

            // copy each file into destination
            foreach (var file in source.GetFiles())
            {
                file.CopyTo(Path.Combine(destination.FullName, file.Name), true);
            }

            // copy each subdirectory using recursion
            foreach (var diSourceSubDir in source.GetDirectories())
            {
                var nextTargetSubDir = destination.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }
    }
}
