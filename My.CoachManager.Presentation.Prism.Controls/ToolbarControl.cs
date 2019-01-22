using System.Windows;
using System.Windows.Controls;

namespace My.CoachManager.Presentation.Prism.Controls
{
    /// <summary>
    /// A content control that allows an _adorner for the content to be defined in XAML.
    /// </summary>
    public class ToolbarControl : ItemsControl
    {
        #region Properties

        #region Direction

        public static readonly DependencyProperty DirectionProperty = DependencyProperty.Register(
            "Direction",
            typeof(FlowDirection),
            typeof(ToolbarControl),
            new FrameworkPropertyMetadata(System.Windows.FlowDirection.LeftToRight));

        public FlowDirection Direction
        {
            get => (FlowDirection)GetValue(DirectionProperty);
            set => SetValue(DirectionProperty, value);
        }

        #endregion

        #region ShowSelection

        public static readonly DependencyProperty ShowSelectionProperty = DependencyProperty.Register(
            "ShowSelection",
            typeof(bool),
            typeof(ToolbarControl),
            new FrameworkPropertyMetadata(true));

        public bool ShowSelection
        {
            get => (bool)GetValue(ShowSelectionProperty);
            set => SetValue(ShowSelectionProperty, value);
        }

        #endregion

        #region ShowActions

        public static readonly DependencyProperty ShowActionsProperty = DependencyProperty.Register(
            "ShowActions",
            typeof(bool),
            typeof(ToolbarControl),
            new FrameworkPropertyMetadata(true));

        public bool ShowActions
        {
            get => (bool)GetValue(ShowActionsProperty);
            set => SetValue(ShowActionsProperty, value);
        }

        #endregion

        #region PlacementTarget

        public static readonly DependencyProperty PlacementTargetProperty = DependencyProperty.Register(
            "PlacementTarget",
            typeof(object),
            typeof(ToolbarControl),
            new FrameworkPropertyMetadata(null));

        public object PlacementTarget
        {
            get => GetValue(PlacementTargetProperty);
            set => SetValue(PlacementTargetProperty, value);
        }

        #endregion

        #endregion

    }
}