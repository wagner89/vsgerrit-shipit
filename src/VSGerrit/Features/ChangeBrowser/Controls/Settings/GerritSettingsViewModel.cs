using System.Windows.Input;
using Microsoft.VisualStudio.PlatformUI;
using VSGerrit.Api.Common.Settings;

namespace VSGerrit.Features.ChangeBrowser.Controls.Settings
{
    public class GerritSettingsViewModel
    {
        public GerritSettingsViewModel()
        {
            var configuration = ConfigurationProvider.Instance.GetConfiguration();

            Username = configuration.UserName;
            Password = configuration.Password;
            GerritApiUrl = configuration.GerritApiUrl;

            ApplyCommand = new DelegateCommand(_ => HandleApplyCommand(), _ => CanExecuteApplyCommand());
        }

        public string Username { get; set; }

        public string Password { get; set; }

        public string GerritApiUrl { get; set; }

        public ICommand ApplyCommand { get; private set; }

        public void HandleApplyCommand()
        {
            ConfigurationProvider.Instance.Initialize(new GerritConfiguration(Username, Password, GerritApiUrl));
        }

        public bool CanExecuteApplyCommand()
        {
            return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(GerritApiUrl);
        }
    }
}
