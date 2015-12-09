using Microsoft.VisualStudio.PlatformUI;
using System.Windows.Input;
using VSGerrit.Features.ChangeBrowser.Services;

namespace VSGerrit.Features.ChangeBrowser.Controls.ButtonBar
{
    public class ButtonBarViewModel
    {
        private readonly IChangeBrowserNavigationService _navService;

        public ButtonBarViewModel(IChangeBrowserNavigationService navService)
        {
            _navService = navService;
            SettingsButtonClickCommand = new DelegateCommand(_ => ToggleSettingsView(), _ => true);
        }

        public ICommand SettingsButtonClickCommand { get; set; }

        

        private void ToggleSettingsView()
        {
            _navService.NavigateToSettings();
        }
    }
}