using System.Windows;
using My.CoachManager.Presentation.Prism.Controls.Collections;

namespace My.CoachManager.Presentation.Prism.Controls.GridViews.Columns
{
    public class ActionsGridViewColumn : ExtendedGridViewColumn
    {
        #region ButtonCommands

        public static readonly DependencyProperty ButtonCommandsProperty = DependencyProperty.Register(
            "ButtonCommands",
            typeof(UiCollection),
            typeof(ActionsGridViewColumn),
            new PropertyMetadata(new UiCollection()));

        public UiCollection ButtonCommands
        {
            get => (UiCollection)GetValue(ButtonCommandsProperty);
            set => SetValue(ButtonCommandsProperty, value);
        }

        #endregion
    }
}
