using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace VSGerrit.Api.Domain
{
    public class ChangeInfo
    {
        public string Id { get; set; }

        public string Project { get; set; }

        public string Branch { get; set; }

        public string Topic { get; set; }

        [JsonProperty("change_id")]
        public string ChangeId { get; set; }

        public string Subject { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ChangeInfoStatus Status { get; set; }

        public string Created { get; set; }

        public string Updated { get; set; }

        public bool Stared { get; set; }

        public bool Reviewed { get; set; }

        public bool Mergeable { get; set; }

        public int Insertions { get; set; }

        public int Deletions { get; set; }

        [JsonProperty("_number")]
        public int Number { get; set; }

        public AccountInfo Owner { get; set; }

        public Dictionary<string, ActionInfo> Actions { get; set; }

        public Labels Labels { get; set; }

        public List<ChangeInfoMessage> Messages { get; set; }

        [JsonProperty("current_revision")]
        public string CurrentRevision { get; set; }

        public Dictionary<string, RevisionInfo> Revisions { get; set; }

        [JsonProperty("_more_changes")]
        public bool MoreChanges { get; set; }

        public List<ProblemInfo> Problems { get; set; }

        [JsonProperty("base_change")]
        public string BaseChange { get; set; }
    }
}
