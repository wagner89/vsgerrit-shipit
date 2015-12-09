using VSGerrit.Common.Models;

namespace VSGerrit.Common.Services
{
    public class ChangeCommentService
    {
        private static readonly ChangeCommentService _instance = new ChangeCommentService();

        public static ChangeCommentService Instance => _instance;

        public ChangeComments GetCommentsForCurrentChange()
        {
            //TODO: hook up to gerrit
            var changeComments = new ChangeComments();
            changeComments.AddFile(new FileWithComments("CommentAdornment.cs", new[]
            {
                new FileComment(2, "This is a comment on the second line."),
                new FileComment(10, "This is a comment on the tenth line. It is very long.")
            }));

            return changeComments;
        }
    }
}