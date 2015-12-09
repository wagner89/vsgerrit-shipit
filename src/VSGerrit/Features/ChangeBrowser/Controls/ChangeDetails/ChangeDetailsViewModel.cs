using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.VisualStudio.PlatformUI;
using VSGerrit.Annotations;
using VSGerrit.Api.Domain;
using VSGerrit.Common.Services;

namespace VSGerrit.Features.ChangeBrowser.Controls.ChangeDetails
{
    public class ChangeDetailsViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly GitService _gitService;
        private readonly VisualStudioWorkspaceService _workspaceService;
        private ChangeInfo _changeInfo;

        public ChangeDetailsViewModel(GitService gitService, VisualStudioWorkspaceService workspaceService)
        {
            _gitService = gitService;
            _workspaceService = workspaceService;
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
            _gitService.Checkout(_workspaceService.Rootdirectory, _workspaceService.RepositoryName, _changeInfo.Revisions.Last().Value.Ref);
        }
    }
}
