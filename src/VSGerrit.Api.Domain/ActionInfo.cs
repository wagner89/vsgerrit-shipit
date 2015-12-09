namespace VSGerrit.Api.Domain
{
    public class ActionInfo
    {
        public string Method { get; set; }

        public string Label { get; set; }

        public string Title { get; set; }

        public bool Enabled { get; set; }
    }
}