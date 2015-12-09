﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using VSGerrit.Annotations;
using VSGerrit.Api.Repositories.Changes;
using VSGerrit.Features.ChangeBrowser.Controls.ButtonBar;
using VSGerrit.Features.ChangeBrowser.Controls.ChangeDetails;
using VSGerrit.Features.ChangeBrowser.Controls.ChangeList;
using VSGerrit.Features.ChangeBrowser.Controls.Settings;
using VSGerrit.Features.ChangeBrowser.Services;

namespace VSGerrit.Features.ChangeBrowser
{
    public class ChangeBrowserViewModel : IChangeBrowserNavigationService, INotifyPropertyChanged
    {
        private bool _isSettingsVisible;

        public ChangeBrowserViewModel()
        {
            ButtonBarViewModel = new ButtonBarViewModel(this);
            ChangeListViewModel = new ChangeListViewModel(this, new ChangeRepository());
            ChangeDetailsViewModel = new ChangeDetailsViewModel();
            GerritSettingsViewModel = new GerritSettingsViewModel();
        }

        public ButtonBarViewModel ButtonBarViewModel { get; private set; }

        public ChangeListViewModel ChangeListViewModel { get; private set; }

        public ChangeDetailsViewModel ChangeDetailsViewModel { get; private set; }

        public GerritSettingsViewModel GerritSettingsViewModel { get; private set; }

        public bool IsSettingsVisible
        {
            get { return _isSettingsVisible; }
            private set
            {
                _isSettingsVisible = value;
                OnPropertyChanged();
            }
        }

        public void NavigateToSettings()
        {
            IsSettingsVisible = !IsSettingsVisible;
        }

        public void NavigateToDetails()
        {
        }

        public void NavigateToList()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}