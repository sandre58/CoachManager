using System.Collections;
using System.Windows;
using System.Windows.Controls;
using My.CoachManager.Presentation.Prism.Core.Commands;

namespace My.CoachManager.Presentation.Prism.Controls
{
    public class ExtendedListViewItem : ListViewItem
    {
        #region ButtonCommands

        /// <summary>
        /// Identifies the BackgroundContent dependency property.
        /// </summary>
        public static readonly DependencyProperty ButtonCommandsProperty = DependencyProperty.Register("ButtonCommands", typeof(IEnumerable), typeof(ExtendedListViewItem), new PropertyMetadata(new CommandsCollection()));

        /// <summary>
        /// Get or set commands in Application Bar.
        /// </summary>
        public IEnumerable ButtonCommands
        {
            get => (IEnumerable)GetValue(ButtonCommandsProperty);

            set => SetValue(ButtonCommandsProperty, value);
        }

        #endregion ButtonCommands

        #region ContextMenuCommands

        /// <summary>
        /// Identifies the BackgroundContent dependency property.
        /// </summary>
        public static readonly DependencyProperty ContextMenuCommandsProperty = DependencyProperty.Register("ContextMenuCommands", typeof(IEnumerable), typeof(ExtendedListViewItem), new PropertyMetadata(new CommandsCollection()));

        /// <summary>
        /// Get or set commands in Application Bar.
        /// </summary>
        public IEnumerable ContextMenuCommands
        {
            get => (IEnumerable)GetValue(ContextMenuCommandsProperty);

            set => SetValue(ContextMenuCommandsProperty, value);
        }

        #endregion ContextMenuCommands
    }
}