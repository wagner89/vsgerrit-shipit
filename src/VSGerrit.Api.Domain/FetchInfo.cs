using System.Collections.Generic;

namespace VSGerrit.Api.Domain
{
    public class FetchInfo
    {
        public string Url { get; set; }

        public string Ref { get; set; }

        public Dictionary<string, string> Commands { get; set; }
    }
}