using System.Collections.Generic;

namespace VSGerrit.Api.Domain
{
    public class VerifiedLabel
    {
        public List<AccountInfoVote> All { get; set; }

        public Dictionary<int, string> Values { get; set; }
    }
}