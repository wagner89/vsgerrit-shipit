using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace VSGerrit.Api.Domain
{
    public class RevisionInfo
    {
        public bool Draft { get; set; }

        [JsonProperty("has_draft_comments")]
        public bool HasDraftComments { get; set; }

        [JsonProperty("_number")]
        public int Number { get; set; }

        public DateTime Created { get; set; }

        public AccountInfo Uploader { get; set; }

        public string Ref { get; set; }

        public Dictionary<string, FetchInfo> Fetch { get; set; }

        public CommitInfo Commit { get; set; }

        public Dictionary<string, FileInfo> Files { get; set; }

        public Dictionary<string, ActionInfo> Actions { get; set; }

        public bool Reviewed { get; set; }
    }
}