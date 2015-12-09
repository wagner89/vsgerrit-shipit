using LibGit2Sharp;

namespace VSGerrit.Features.ChangeBrowser.Services
{
    public class GitService
    {
        public void Checkout(string commithash)
        {
            //var repository = new Repository(filePath);
            //repository.Checkout(commithash);
        }

        private string GetCurrentDir()
        {
            return string.Empty;
        }
    }
}
