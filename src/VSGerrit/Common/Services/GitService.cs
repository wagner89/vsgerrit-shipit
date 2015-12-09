using System.Diagnostics;
using System.IO;
using System.Linq;

namespace VSGerrit.Common.Services
{
    public class GitService
    {
        public void Checkout(string filePath, string refs)
        {
            var fileInfo = new FileInfo(filePath);
            var rootDirectory = GetGitRootDirectory(fileInfo.Directory);

            RunGit(rootDirectory, $"fetch ssh://czaharia@gerrit.ullink.lan:29418/{rootDirectory.Name} {refs}");
            RunGit(rootDirectory, "checkout FETCH_HEAD");
        }

        private static void RunGit(FileSystemInfo rootDirectory, string gitArguments)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    WorkingDirectory = rootDirectory.FullName,
                    FileName = "git.exe",
                    Arguments = gitArguments,
                    CreateNoWindow = true
                }
            };

            process.Start();
            process.WaitForExit();
        }

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
