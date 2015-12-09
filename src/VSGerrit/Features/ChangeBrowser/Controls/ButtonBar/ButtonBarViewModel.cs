using Microsoft.VisualStudio.PlatformUI;
using System.Windows.Input;
using VSGerrit.Features.ChangeBrowser.Services;

namespace VSGerrit.Features.ChangeBrowser.Controls.ButtonBar
{
    public class ButtonBarViewModel
    {
        private readonly IChangeBrowserNavigationService _navigationService;

        public ButtonBarViewModel(IChangeBrowserNavigationService navigationService)
        {
            _navigationService = navigationService;

            NavigateToSetingsCommand = new DelegateCommand(_ => HandleNavigateToSettingsCommand(), _ => true);
        }

        public ICommand NavigateToSetingsCommand { get; set; }

        private void HandleNavigateToSettingsCommand()
        {
            _navigationService.NavigateToSettings();
        }
    }
}