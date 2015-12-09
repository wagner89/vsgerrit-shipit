using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSGerrit.Common.Models
{
    public class ChangeComments
    {
        private readonly IList<FileWithComments> _filesWithComments;

        public ChangeComments()
        {
            _filesWithComments = new List<FileWithComments>();
        }

        public void Clear()
        {
            _filesWithComments.Clear();
        }

        public void AddFile(FileWithComments fileWithComments)
        {
            _filesWithComments.Add(fileWithComments);
        }


        public IEnumerable<string> CommentedFiles
        {
            get { return _filesWithComments.Select(file => file.Filename); }
        }

        public IEnumerable<FileComment> GetCommentsForFile(string filename)
        {
            return _filesWithComments.First(file => file.Filename.Contains(filename)).Comments;
        }
    }
}
