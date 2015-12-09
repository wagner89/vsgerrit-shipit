using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using VSGerrit.Annotations;

namespace VSGerrit.Common.Commands
{
    public class CommandRelay : ICommand, INotifyPropertyChanged
    {
        ICommand _value;

        public event EventHandler<ValueEventArgs<ICommand>> Initialize;

        public ICommand Command
        {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }

        bool ICommand.CanExecute(object parameter)
        {
            if (_value == null && !InitializeCommand())
                return false;

            return _value.CanExecute(parameter);
        }

        event EventHandler ICommand.CanExecuteChanged
        {
            add
            {
                if (_value != null || InitializeCommand())
                    _value.CanExecuteChanged += value;
            }
            remove
            {
                if (_value != null || InitializeCommand())
                    _value.CanExecuteChanged -= value;
            }
        }

        void ICommand.Execute(object parameter)
        {
            if (_value != null || InitializeCommand())
                _value.Execute(parameter);
        }

        private bool InitializeCommand()
        {
            if (Command != null)
                return true;

            if (Initialize == null)
                return (Command != null);

            var eventArgs = new ValueEventArgs<ICommand>();
            Initialize(this, eventArgs);
            Command = eventArgs.Value;

            return Command != null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
