using VSGerrit.Features.ChangeBrowser.Controls.ButtonBar;
using VSGerrit.Features.ChangeBrowser.Services;

namespace VSGerrit.Features.ChangeBrowser
{
    public class ChangeBrowserViewModel
    {
        public ChangeBrowserViewModel()
        {
            var navigationService = new ChangeBrowserNavigationService();
            ButtonBarViewModel = new ButtonBarViewModel(navigationService);
        }

        public ButtonBarViewModel ButtonBarViewModel { get; set; }
    }
}