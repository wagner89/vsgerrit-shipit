namespace VSGerrit.Common.Models
{
    public class FileComment
    {
        public FileComment(int lineNumber, string commentText)
        {
            LineNumber = lineNumber;
            CommentText = commentText;
        }

        public int LineNumber { get; }

        public string CommentText { get; }
    }
}