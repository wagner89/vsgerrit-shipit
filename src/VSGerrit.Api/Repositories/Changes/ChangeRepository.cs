using System.Collections.Generic;
using RestSharp;
using VSGerrit.Api.Domain;

namespace VSGerrit.Api.Repositories.Changes
{
    public class ChangeRepository : RepositoryBase, IChangeRepository
    {
        private readonly ChangeQueryStringBuilder queryStringBuilder = new ChangeQueryStringBuilder();

        public List<ChangeInfo> GetAll(ChangeQueryParameters queryParameters, ChangeOptionalParameters optionalParameters)
        {
            var restRequest = GetRestRequest("/changes", queryParameters, optionalParameters);

            return ExecuteRequest<List<ChangeInfo>>(restRequest);
        }

        public ChangeInfo Get(string changeId, ChangeQueryParameters queryParameters, ChangeOptionalParameters optionalParameters)
        {
            var restRequest = GetRestRequest($"/changes/{changeId}", queryParameters, optionalParameters);

            return ExecuteRequest<ChangeInfo>(restRequest);
        }

        public ChangeInfo GetDetails(string changeId, ChangeQueryParameters queryParameters, ChangeOptionalParameters optionalParameters)
        {
            var restRequest = GetRestRequest($"/changes/{changeId}/detail", queryParameters, optionalParameters);

            return ExecuteRequest<ChangeInfo>(restRequest);
        }

        public string GetTopic(string changeId)
        {
            var restRequest = GetRestRequest($"/changes/{changeId}/topic");

            return ExecuteRequest<string>(restRequest);
        }

        public Dictionary<string, List<CommentInfo>> GetComments(string changeId)
        {
            var restRequest = GetRestRequest($"/changes/{changeId}/comments");

            return ExecuteRequest<Dictionary<string, List<CommentInfo>>>(restRequest);
        }

        public Dictionary<string, List<CommentInfo>> GetDraftComments(string changeId)
        {
            var restRequest = GetRestRequest($"/changes/{changeId}/drafts");

            return ExecuteRequest<Dictionary<string, List<CommentInfo>>>(restRequest);
        }

        public ChangeInfo CheckChange(string changeId)
        {
            var restRequest = GetRestRequest($"/changes/{changeId}/check");

            return ExecuteRequest<ChangeInfo>(restRequest);
        }

        private RestRequest GetRestRequest(string url)
        {
            return GetRestRequest(url, ChangeQueryParameters.Empty, ChangeOptionalParameters.Empty);
        }

        private RestRequest GetRestRequest(string url, ChangeQueryParameters queryParameters, ChangeOptionalParameters optionalParameters)
        {
            var queryString = queryStringBuilder.GetQueryString(queryParameters, optionalParameters);
            return new RestRequest($"{url}/{queryString}", Method.GET);
        }
    }
}
