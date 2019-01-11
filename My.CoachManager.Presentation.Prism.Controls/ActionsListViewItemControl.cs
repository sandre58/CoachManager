using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace My.CoachManager.Presentation.Prism.Controls
{
    /// <summary>
    /// A content control that allows an _adorner for the content to be defined in XAML.
    /// </summary>
    public class ActionsListViewItemControl : ItemsControl
    {
        #region Properties

        #region ShowSelection

        public static readonly DependencyProperty ShowSelectionProperty = DependencyProperty.Register(
            "ShowSelection",
            typeof(bool),
            typeof(ActionsListViewItemControl),
            new FrameworkPropertyMetadata(true));

        public bool ShowSelection
        {
            get => (bool)GetValue(ShowSelectionProperty);
            set => SetValue(ShowSelectionProperty, value);
        }

        #endregion

        #endregion

    }
}