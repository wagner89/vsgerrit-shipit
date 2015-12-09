using Microsoft.VisualStudio.PlatformUI;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using VSGerrit.Annotations;

namespace VSGerrit.Features.Adornment.CommentPopup
{
    public class CommentPopupViewModel : INotifyPropertyChanged
    {
        private bool _isCommentVisible;

        public CommentPopupViewModel(string comment = "")
        {
            Comment = comment;
            ChangeVisibilityCommand = new DelegateCommand((_) => { IsCommentVisible = !IsCommentVisible; });
        }

        public string Comment { get; set; }

        public bool IsCommentVisible
        {
            get { return _isCommentVisible; }

            private set
            {
                _isCommentVisible = value;
                OnPropertyChanged();
            }
        }

        public DelegateCommand ChangeVisibilityCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}