using System.Windows.Controls;

namespace VSGerrit.Features.Adornment.CommentPopup
{
    /// <summary>
    /// Interaction logic for CommentPopup.xaml
    /// </summary>
    public partial class CommentPopup : UserControl
    {
        public CommentPopup()
        {
            InitializeComponent();
            DataContext = new CommentPopupViewModel("Test");
        }

        public CommentPopup(CommentPopupViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}