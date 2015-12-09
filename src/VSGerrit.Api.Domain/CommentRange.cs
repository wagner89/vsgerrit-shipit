using Newtonsoft.Json;

namespace VSGerrit.Api.Domain
{
    public class CommentRange
    {
        [JsonProperty("start_line")]
        public int StartLine { get; set; }

        [JsonProperty("start_character")]
        public int StartCharacter { get; set; }

        [JsonProperty("end_line")]
        public int EndLine { get; set; }

        [JsonProperty("end_character")]
        public int EndCharacter { get; set; }
    }
}