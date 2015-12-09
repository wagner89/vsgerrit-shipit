using System.Collections.Generic;
using RestSharp;
using VSGerrit.Api.Domain;

namespace VSGerrit.Api.Repositories.Revisions
{
    public class RevisionRepository : RepositoryBase, IRevisionRepository
    {
        public CommitInfo GetCommit(string changeId, string revisionId)
        {
            var restRequest = new RestRequest($"/changes/{changeId}/revisions/{revisionId}/commit", Method.GET);

            return ExecuteRequest<CommitInfo>(restRequest);
        }

        public Dictionary<string, ActionInfo> GetRevisionActions(string changeId, string revisionId)
        {
            var restRequest = new RestRequest($"/changes/{changeId}/revisions/{revisionId}/actions", Method.GET);

            return ExecuteRequest<Dictionary<string, ActionInfo>>(restRequest);
        }

        public ChangeInfo GetReview(string changeId, string revisionId)
        {
            var restRequest = new RestRequest($"/changes/{changeId}/revisions/{revisionId}/review", Method.GET);

            return ExecuteRequest<ChangeInfo>(restRequest);
        }

        public Dictionary<string, List<CommentInfo>> GetComments(string changeId, string revisionId)
        {
            var restRequest = new RestRequest($"/changes/{changeId}/revisions/{revisionId}/comments", Method.GET);

            return ExecuteRequest<Dictionary<string, List<CommentInfo>>>(restRequest);
        }
    }
}
