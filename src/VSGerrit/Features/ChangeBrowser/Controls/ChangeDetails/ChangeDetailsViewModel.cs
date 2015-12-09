using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.VisualStudio.PlatformUI;
using VSGerrit.Annotations;
using VSGerrit.Api.Domain;
using VSGerrit.Features.ChangeBrowser.Services;

namespace VSGerrit.Features.ChangeBrowser.Controls.ChangeDetails
{
    public class ChangeDetailsViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ChangeInfo _changeInfo;

        public ChangeDetailsViewModel()
        {
        }

        public ChangeInfo ChangeInfo
        {
            get { return _changeInfo; }
            set
            {
                _changeInfo = value;
                OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
