using Newtonsoft.Json;

namespace VSGerrit.Api.Domain
{
    public class FileInfo
    {
        public string Status { get; set; }

        public bool Binary { get; set; }

        [JsonProperty("old_path")]
        public string OldPath { get; set; }

        [JsonProperty("lines_inserted")]
        public int LinesInserted { get; set; }

        [JsonProperty("lines_deleted")]
        public int LinesDeleted { get; set; }
    }
}