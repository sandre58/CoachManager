using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace My.CoachManager.Presentation.Uwp.Controls
{
    public class WorkspaceView : ContentControl
    {

        #region Title

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(WorkspaceView), new PropertyMetadata(null));
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        #endregion

        #region RightContent

        public static readonly DependencyProperty RightContentProperty = DependencyProperty.Register("RightContent", typeof(object), typeof(WorkspaceView), new PropertyMetadata(null));
        public object RightContent
        {
            get => GetValue(RightContentProperty);
            set => SetValue(RightContentProperty, value);
        }

        #endregion

        #region CommandBar

        public static readonly DependencyProperty CommandBarProperty = DependencyProperty.Register("CommandBar", typeof(CommandBar), typeof(WorkspaceView), new PropertyMetadata(null));
        public CommandBar CommandBar
        {
            get => (CommandBar)GetValue(CommandBarProperty);
            set => SetValue(CommandBarProperty, value);
        }

        #endregion

    }
}
