using Microsoft.VisualStudio.PlatformUI;
using System.Windows.Input;

namespace VSGerrit.Features.ChangeBrowser.Controls.ButtonBar
{
    public class ButtonBarViewModel
    {
        public ButtonBarViewModel()
        {
            SettingsButtonClickCommand = new DelegateCommand(_ => ToggleSettingsView(), (_) => true);
        }

        public ICommand SettingsButtonClickCommand { get; set; }

        private void ToggleSettingsView()
        {
            // TODO
        }
    }
}