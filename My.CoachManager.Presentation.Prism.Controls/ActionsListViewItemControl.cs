using System.Collections.Generic;
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

        #region MenuItems

        public static readonly DependencyProperty MenuItemsProperty = DependencyProperty.Register(
            "MenuItems",
            typeof(List<MenuItem>),
            typeof(ActionsListViewItemControl),
            new FrameworkPropertyMetadata(new List<MenuItem>()));

        public List<MenuItem> MenuItems
        {
            get => (List<MenuItem>)GetValue(MenuItemsProperty);
            set => SetValue(MenuItemsProperty, value);
        }

        #endregion

        #endregion

    }
}