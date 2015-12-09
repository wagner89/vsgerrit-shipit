using VSGerrit.Common.Models;

namespace VSGerrit.Common.Services
{
    public class ChangeCommentService
    {
        private ChangeComments _changeComments = new ChangeComments();

        public static ChangeCommentService Instance { get; } = new ChangeCommentService();

        public ChangeComments GetCommentsForCurrentChange()
        {
            return _changeComments;
        }

        public void UpdateChangeComments(ChangeComments changeComments)
        {
            _changeComments = changeComments;
        }
    }
}