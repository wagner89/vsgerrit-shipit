using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.VisualStudio.PlatformUI;
using VSGerrit.Annotations;
using VSGerrit.Api.Domain;
using VSGerrit.Features.ChangeBrowser.Services;

namespace VSGerrit.Features.ChangeBrowser.Controls.ChangeDetails
{
    public class ChangeDetailsViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly GitService _gitService;
        private ChangeInfo _changeInfo;

        public ChangeDetailsViewModel(GitService gitService)
        {
            _gitService = gitService;
            StartReviewCommand = new DelegateCommand(_ => HandleStartReviewCommand());
        }

        public ICommand StartReviewCommand { get; private set; }

        public ChangeInfo ChangeInfo
        {
            get { return _changeInfo; }
            set
            {
                _changeInfo = value;

                OnPropertyChanged();
                OnPropertyChanged(nameof(CanStartReview));
            }
        }

        public bool CanStartReview => ChangeInfo != null;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void HandleStartReviewCommand()
        {
            _gitService.Checkout(_changeInfo.CurrentRevision);
        }
    }
}
