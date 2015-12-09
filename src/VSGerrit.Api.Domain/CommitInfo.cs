using System.Collections.Generic;

namespace VSGerrit.Api.Domain
{
    public class CommitInfo
    {
        public string Commit { get; set; }

        public List<CommitInfo> Parents { get; set; }

        public GitPersonInfo Author { get; set; }

        public GitPersonInfo Committer { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }

        // TODO: web_links
    }
}