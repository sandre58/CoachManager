using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace My.CoachManager.Presentation.Controls.Dialogs
{
    public class CustomDialog : BaseMetroDialog
    {
        #region Constructors

        public CustomDialog()
            : this(null)
        {
        }

        public CustomDialog(MetroDialogSettings settings)
            : this(null, settings)
        {
        }

        public CustomDialog(MetroWindow parentWindow, MetroDialogSettings settings = null)
            : base(parentWindow, settings)
        {
        }

        #region Static Fields

        public static readonly DependencyProperty CloseCommandProperty = DependencyProperty.Register("CloseCommand", typeof(ICommand), typeof(CustomDialog));
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(Style), typeof(CustomDialog));

        #endregion Static Fields

        #region Properties

        /// <summary>
        /// Get or set the command on the Close button.
        /// </summary>
        public ICommand CloseCommand
        {
            get
            {
                return (ICommand)GetValue(CloseCommandProperty);
            }

            set
            {
                SetValue(CloseCommandProperty, value);
            }
        }

        /// <summary>
        /// Get or set the icon.
        /// </summary>
        public Style Icon
        {
            get
            {
                return (Style)GetValue(IconProperty);
            }

            set
            {
                SetValue(IconProperty, value);
            }
        }

        #endregion Properties

        #endregion Constructors
    }
}