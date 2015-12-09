using System.Collections.Generic;

namespace VSGerrit.Common.Models
{
    public class FileWithComments
    {
        public FileWithComments(string filename, IEnumerable<FileComment> comments)
        {
            Filename = filename;
            Comments = comments;
        }

        public string Filename { get; }

        public IEnumerable<FileComment> Comments { get; }
    }
}