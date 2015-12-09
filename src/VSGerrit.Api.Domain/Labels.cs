using Newtonsoft.Json;

namespace VSGerrit.Api.Domain
{
    public class Labels
    {
        [JsonProperty("Code-Review")]
        public CodeReviewLabel CodeReview { get; set; }

        public VerifiedLabel Verified { get; set; }
    }
}