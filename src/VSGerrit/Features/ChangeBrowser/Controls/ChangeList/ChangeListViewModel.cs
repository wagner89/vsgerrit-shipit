using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Microsoft.VisualStudio.PlatformUI;
using VSGerrit.Api.Domain;
using VSGerrit.Api.Repositories.Changes;
using VSGerrit.Common.Services;
using VSGerrit.Features.ChangeBrowser.Services;

namespace VSGerrit.Features.ChangeBrowser.Controls.ChangeList
{
    public class ChangeListViewModel
    {
        private readonly IChangeBrowserNavigationService _navigationService;

        public ChangeListViewModel(IChangeBrowserNavigationService navigationService, IChangeRepository changeRepository)
        {
            _navigationService = navigationService;

            var queryParameters = new ChangeQueryParameters
            {
                ReviewedByMe = true,
                NumberOfResults = 10
            };

            var optionalParameters = new ChangeOptionalParameters
            {
                DetailedAccounts = true,
                CurrentRevision = true,
                AllFiles = true,
                AllRevisions = true
            };

            var projectName = VisualStudioWorkspaceService.Instance.RepositoryName;
            Changes = changeRepository.GetAll(queryParameters, optionalParameters).Where(changeInfo => changeInfo.Project == projectName).ToList();

            ChangeSelectedCommand = new DelegateCommand(changeInfo => HandleChangeSelectedCommand((ChangeInfo)changeInfo));
        }

        public ICommand ChangeSelectedCommand { get; private set; }

        public List<ChangeInfo> Changes { get; set; }

        private void HandleChangeSelectedCommand(ChangeInfo changeInfo)
        {
            _navigationService.NavigateToDetails(changeInfo);
        }
    }
}
