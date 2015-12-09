//------------------------------------------------------------------------------
// <copyright file="ChangeBrowserControlControl.xaml.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace VSGerrit.Features.ChangeBrowser
{
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for ChangeBrowserControlControl.
    /// </summary>
    public partial class ChangeBrowserControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeBrowserControl"/> class.
        /// </summary>
        public ChangeBrowserControl()
        {
            InitializeComponent();

            DataContext = new ChangeBrowserViewModel();
        }
    }
}