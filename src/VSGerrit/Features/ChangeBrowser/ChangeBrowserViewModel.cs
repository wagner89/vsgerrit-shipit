using VSGerrit.Api.Repositories.Changes;
using VSGerrit.Features.ChangeBrowser.Controls.ButtonBar;
using VSGerrit.Features.ChangeBrowser.Controls.ChangeList;
using VSGerrit.Features.ChangeBrowser.Services;

namespace VSGerrit.Features.ChangeBrowser
{
    public class ChangeBrowserViewModel
    {
        public ChangeBrowserViewModel()
        {
            var navigationService = new ChangeBrowserNavigationService();

            ButtonBarViewModel = new ButtonBarViewModel(navigationService);
            ChangeListViewModel = new ChangeListViewModel(navigationService, new ChangeRepository());
        }

        public ButtonBarViewModel ButtonBarViewModel { get; private set; }

        public ChangeListViewModel ChangeListViewModel { get; private set; }
    }
}