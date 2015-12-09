using System.Collections.Generic;
using VSGerrit.Api.Domain;

namespace VSGerrit.Api.Repositories.Changes
{
    public interface IChangeRepository
    {
        List<ChangeInfo> GetAll(ChangeQueryParameters queryParameters, ChangeOptionalParameters optionalParameters);

        ChangeInfo Get(string changeId, ChangeQueryParameters queryParameters, ChangeOptionalParameters optionalParameters);

        ChangeInfo GetDetails(string changeId, ChangeQueryParameters queryParameters, ChangeOptionalParameters optionalParameters);

        string GetTopic(string changeId);

        Dictionary<string, List<CommentInfo>> GetComments(string changeId);

        Dictionary<string, List<CommentInfo>> GetDraftComments(string changeId);

        ChangeInfo CheckChange(string changeId);
    }
}
