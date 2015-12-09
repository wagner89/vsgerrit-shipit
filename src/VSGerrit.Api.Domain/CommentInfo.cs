using System;
using Newtonsoft.Json;

namespace VSGerrit.Api.Domain
{
    public class CommentInfo
    {
        public string Id { get; set; }

        public string Path { get; set; }

        public string Side { get; set; }

        public int Line { get; set; }

        public string Range { get; set; }

        [JsonProperty("in_reply_to")]
        public string InReplyTo { get; set; }

        public string Message { get; set; }

        public DateTime Updated { get; set; }

        public AccountInfo Author { get; set; }
    }
}
