using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.LanguageServices;

namespace VSGerrit.Common.Services
{
    public class VisualStudioWorkspaceService
    {
        private static readonly Lazy<VisualStudioWorkspaceService> _instance = new Lazy<VisualStudioWorkspaceService>(() => new VisualStudioWorkspaceService());

        public static VisualStudioWorkspaceService Instance => _instance.Value;

        public VisualStudioWorkspace Workspace { get; set; }

        public string RepositoryName => GetGitRootDirectory(new FileInfo(Workspace.CurrentSolution.FilePath).Directory).Name;

        public string Rootdirectory => GetGitRootDirectory(new FileInfo(Workspace.CurrentSolution.FilePath).Directory).FullName;

        private static DirectoryInfo GetGitRootDirectory(DirectoryInfo path)
        {
            while (true)
            {
                if (path != null && path.GetDirectories().Select(dir => dir.Name).Contains(".git"))
                {
                    return path;
                }

                if (path == null)
                    return null;

                path = path.Parent;
            }
        }

    }
}
