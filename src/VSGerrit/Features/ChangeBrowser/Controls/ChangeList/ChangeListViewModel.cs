using VSGerrit.Features.ChangeBrowser.Services;

namespace VSGerrit.Features.ChangeBrowser.Controls.ChangeList
{
    public class ChangeListViewModel
    {
        private readonly IChangeBrowserNavigationService _navigationService;

        public ChangeListViewModel(IChangeBrowserNavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
