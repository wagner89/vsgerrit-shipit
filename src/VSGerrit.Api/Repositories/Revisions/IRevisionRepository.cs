using System.Collections.Generic;
using VSGerrit.Api.Domain;

namespace VSGerrit.Api.Repositories.Revisions
{
    public interface IRevisionRepository
    {
        CommitInfo GetCommit(string changeId, string revisionId);

        Dictionary<string, ActionInfo> GetRevisionActions(string changeId, string revisionId);

        ChangeInfo GetReview(string changeId, string revisionId);
    }
}