using Newtonsoft.Json;
using RestSharp;
using VSGerrit.Api.Authentication;
using VSGerrit.Api.Common.Settings;

namespace VSGerrit.Api.Repositories
{
    public class RepositoryBase
    {
        private const string SecurityPrefix = ")]}'";

        public TEntity ExecuteRequest<TEntity>(RestRequest restRequest)
        {
            var configurationProvider = ConfigurationProvider.Instance.GetConfiguration();
            var restClient = new RestClient(configurationProvider.GerritApiUrl)
            {
                Authenticator = new DigestAuthenticator(configurationProvider.UserName, configurationProvider.Password)
            };

            var response = restClient.Execute(restRequest);
            var json = GetJson(response.Content);

            return JsonConvert.DeserializeObject<TEntity>(json);
        }

        private static string GetJson(string responseContent)
        {
            var content = responseContent.StartsWith(SecurityPrefix)
                ? responseContent.Remove(0, SecurityPrefix.Length)
                : responseContent;

            return content.Trim('\n');
        }
    }
}
