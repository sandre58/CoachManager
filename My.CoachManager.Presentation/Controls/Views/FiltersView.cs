using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace My.CoachManager.Presentation.Controls.Views
{
    /// <summary>
    /// A filter control.
    /// </summary>
    public class FiltersView : ContentControl
    {
        #region Static Fields

        public static readonly DependencyProperty FilterCommandProperty = DependencyProperty.Register("FilterCommand", typeof(ICommand), typeof(FiltersView));

        #endregion Static Fields

        #region Constructors and Destructors

        static FiltersView()
        {
            IsTabStopProperty.OverrideMetadata(typeof(FiltersView), new FrameworkPropertyMetadata(false));
            FocusableProperty.OverrideMetadata(typeof(FiltersView), new FrameworkPropertyMetadata(false));
        }

        #endregion Constructors and Destructors

        #region Properties

        /// <summary>
        /// Get or set the command on the Filter button.
        /// </summary>
        public ICommand FilterCommand
        {
            get
            {
                return (ICommand)GetValue(FilterCommandProperty);
            }

            set
            {
                SetValue(FilterCommandProperty, value);
            }
        }

        #endregion Properties
    }
}