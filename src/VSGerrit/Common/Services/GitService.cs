using System.Diagnostics;

namespace VSGerrit.Common.Services
{
    public class GitService
    {
        public void Checkout(string rootDirectory, string repositoryName, string reference)
        {
            RunGitCommand(rootDirectory, $"fetch ssh://czaharia@gerrit.ullink.lan:29418/{repositoryName} {reference}");
            RunGitCommand(rootDirectory, "checkout FETCH_HEAD");
        }

        private static void RunGitCommand(string rootDirectory, string gitCommand)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    WorkingDirectory = rootDirectory,
                    FileName = "git.exe",
                    Arguments = gitCommand,
                    CreateNoWindow = true
                }
            };

            process.Start();
            process.WaitForExit();
        }
    }
}
