using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.VisualStudio.PlatformUI;
using VSGerrit.Annotations;
using VSGerrit.Api.Domain;
using VSGerrit.Api.Repositories.Revisions;
using VSGerrit.Common.Models;
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
            _gitService.Checkout(_workspaceService.Rootdirectory, _workspaceService.RepositoryName, _changeInfo.Revisions[_changeInfo.CurrentRevision].Ref);

            OpenModifiedFiles();

            UpdateVisibleComments();
        }

        private void UpdateVisibleComments()
        {
            var revisionRepository = new RevisionRepository();

            var currentRevisionNumber = ChangeInfo.Revisions[ChangeInfo.CurrentRevision].Number.ToString();
            var comments = revisionRepository.GetComments(ChangeInfo.ChangeId, currentRevisionNumber);

            var result = new ChangeComments();
            comments.Select(file => new FileWithComments(file.Key, file.Value.Select(comment => new FileComment(comment.Range.StartLine, comment.Message))))
                .ToList()
                .ForEach(result.AddFile);

            ChangeCommentService.Instance.UpdateChangeComments(result);
        }

        private void OpenModifiedFiles()
        {
            var rootDirectory = _workspaceService.Rootdirectory;
            var changedFiles = _changeInfo.Revisions[_changeInfo.CurrentRevision].Files.Keys.Select(filename => Path.Combine(rootDirectory, filename).NormalizePath());

            var documentIds = changedFiles.SelectMany(changedFile => _workspaceService.Workspace.CurrentSolution.GetDocumentIdsWithFilePath(changedFile)).ToList();

            foreach (var documentId in documentIds)
            {
                _workspaceService.Workspace.OpenDocument(documentId);
            }
        }
    }
}
