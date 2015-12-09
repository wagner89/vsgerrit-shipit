using System.Collections.Generic;

namespace VSGerrit.Api.Domain
{
    public class CodeReviewLabel
    {
        public List<AccountInfoVote> All { get; set; }

        public AccountInfo Approved { get; set; }

        public Dictionary<int, string> Values { get; set; }
    }
}