using System.Windows;

namespace My.CoachManager.Presentation.Controls
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
    }
}