using System;

namespace VSGerrit.Api.Domain
{
    public class GitPersonInfo
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime Date { get; set; }

        // TODO: tz
    }
}