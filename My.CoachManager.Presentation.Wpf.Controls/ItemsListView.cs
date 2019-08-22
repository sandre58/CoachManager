using System.Windows;
using System.Windows.Input;

namespace My.CoachManager.Presentation.Wpf.Controls
{
    public class ItemsListView : ExtendedListView
    {
        #region CustomSelectionModeProperty

        public static readonly DependencyProperty CustomSelectionModeProperty = DependencyProperty.Register(
            "CustomSelectionMode",
            typeof(Core.Enums.SelectionMode),
            typeof(ItemsListView),
            new PropertyMetadata(Core.Enums.SelectionMode.Single));

        public Core.Enums.SelectionMode CustomSelectionMode
        {
            get => (Core.Enums.SelectionMode)GetValue(CustomSelectionModeProperty);
            set => SetValue(CustomSelectionModeProperty, value);
        }

        #endregion CustomSelectionModeProperty

        #region CanOrderProperty

        public static readonly DependencyProperty CanOrderProperty = DependencyProperty.Register(
            "CanOrder",
            typeof(bool),
            typeof(ItemsListView),
            new PropertyMetadata(false));

        public bool CanOrder
        {
            get => (bool)GetValue(CanOrderProperty);
            set => SetValue(CanOrderProperty, value);
        }

        #endregion CanOrderProperty

        #region EditCommand

        public static readonly DependencyProperty EditCommandProperty = DependencyProperty.Register(
            "EditCommand",
            typeof(ICommand),
            typeof(ItemsListView));

        public ICommand EditCommand
        {
            get => (ICommand)GetValue(EditCommandProperty);
            set => SetValue(EditCommandProperty, value);
        }

        #endregion EditCommand

        #region RemoveCommand

        public static readonly DependencyProperty RemoveCommandProperty = DependencyProperty.Register(
            "RemoveCommand",
            typeof(ICommand),
            typeof(ItemsListView));

        public ICommand RemoveCommand
        {
            get => (ICommand)GetValue(RemoveCommandProperty);
            set => SetValue(RemoveCommandProperty, value);
        }

        #endregion RemoveCommand
    }
}