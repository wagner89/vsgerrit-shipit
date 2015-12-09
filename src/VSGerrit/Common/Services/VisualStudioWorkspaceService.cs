using System;
using Microsoft.VisualStudio.LanguageServices;

namespace VSGerrit.Common.Services
{
    public class VisualStudioWorkspaceService
    {
        private static readonly Lazy<VisualStudioWorkspaceService> _instance = new Lazy<VisualStudioWorkspaceService>(() => new VisualStudioWorkspaceService());

        public static VisualStudioWorkspaceService Instance => _instance.Value;

        public VisualStudioWorkspace Workspace { get; set; }
    }
}
