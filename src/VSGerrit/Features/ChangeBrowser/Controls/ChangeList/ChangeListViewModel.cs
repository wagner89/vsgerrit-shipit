using System.Collections.Generic;
using System.Windows.Input;
using Microsoft.VisualStudio.PlatformUI;
using VSGerrit.Api.Domain;
using VSGerrit.Api.Repositories.Changes;
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

            Changes = changeRepository.GetAll(queryParameters, new ChangeOptionalParameters {DetailedAccounts = true});

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
