using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Elysium.Controls.Primitives;

namespace My.CoachManager.Presentation.Controls.Views
{
    /// <summary>
    /// A filter control.
    /// </summary>
    public class WorkspaceView : ContentControl
    {
        #region Static Fields

        public static readonly DependencyProperty CommandsProperty = DependencyProperty.Register("Commands", typeof(ObservableCollection<CommandButtonBase>), typeof(WorkspaceView));

        #endregion Static Fields

        #region Constructors and Destructors

        static WorkspaceView()
        {
            IsTabStopProperty.OverrideMetadata(typeof(WorkspaceView), new FrameworkPropertyMetadata(false));
            FocusableProperty.OverrideMetadata(typeof(WorkspaceView), new FrameworkPropertyMetadata(false));
        }

        /// <summary>
        /// Initialise une nouvelle instance de <see cref="WorkspaceView"/>.
        /// </summary>
        public WorkspaceView()
        {
            if (System.Windows.Application.Current != null)
                Style = (Style)System.Windows.Application.Current.FindResource(typeof(WorkspaceView));

            Commands = new ObservableCollection<CommandButtonBase>();
        }

        #endregion Constructors and Destructors

        #region Properties

        /// <summary>
        /// Get or set commands in Application Bar.
        /// </summary>
        public ObservableCollection<CommandButtonBase> Commands
        {
            get
            {
                return (ObservableCollection<CommandButtonBase>)GetValue(CommandsProperty);
            }

            set
            {
                SetValue(CommandsProperty, value);
            }
        }

        #endregion Properties
    }
}