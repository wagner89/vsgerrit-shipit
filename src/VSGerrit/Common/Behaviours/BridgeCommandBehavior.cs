using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using VSGerrit.Common.Commands;

namespace VSGerrit.Common.Behaviours
{
    public class BridgeCommandBehavior : Behavior<DependencyObject>
    {
        public static readonly DependencyProperty CommandRelayProperty =
            DependencyProperty.Register("CommandRelay", typeof (CommandRelay), typeof (BridgeCommandBehavior),
                new PropertyMetadata(null, OnCommandRelayChanged));

        public static readonly DependencyProperty CommandSourceProperty =
            DependencyProperty.Register("CommandSource", typeof (ICommand), typeof (BridgeCommandBehavior),
                new PropertyMetadata(null, OnCommandSourceChanged));

        [Category("Common Properties")]
        public CommandRelay CommandRelay
        {
            get { return (CommandRelay) GetValue(CommandRelayProperty); }
            set { SetValue(CommandRelayProperty, value); }
        }

        [Category("Common Properties")]
        [CustomPropertyValueEditor(CustomPropertyValueEditor.PropertyBinding)]
        public ICommand CommandSource
        {
            get { return (ICommand) GetValue(CommandSourceProperty); }
            set { SetValue(CommandSourceProperty, value); }
        }

        private static void OnCommandSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BridgeCommandBehavior) d).UpdateRelay();
        }

        private static void OnCommandRelayChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BridgeCommandBehavior) d).UpdateRelay();
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            UpdateRelay();
        }

        private void UpdateRelay()
        {
            if (CommandRelay != null && CommandSource != null)
            {
                CommandRelay.Command = CommandSource;
            }
        }
    }
}