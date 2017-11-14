using System.Collections.ObjectModel;
using System.Windows;
using Elysium.Controls;
using Elysium.Controls.Primitives;
using My.CoachManager.CrossCutting.Core.Extensions;

namespace My.CoachManager.Presentation.Controls
{
    /// <summary>
    /// A filter control.
    /// </summary>
    public class DefaultApplicationBar : ApplicationBar
    {
        #region Fields

        public static readonly DependencyProperty DefaultCommmandsProperty = DependencyProperty.Register("DefaultCommmands", typeof(ObservableCollection<CommandButtonBase>), typeof(DefaultApplicationBar));

        #endregion Fields

        #region Constructors

        public DefaultApplicationBar()
        {
            DefaultCommmands = new ObservableCollection<CommandButtonBase>();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Get or Set the default Commands.
        /// </summary>
        public ObservableCollection<CommandButtonBase> DefaultCommmands
        {
            get
            {
                return (ObservableCollection<CommandButtonBase>)GetValue(DefaultCommmandsProperty);
            }

            set
            {
                SetValue(DefaultCommmandsProperty, value);
            }
        }

        #endregion Properties

        #region Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var items = new ObservableCollection<CommandButtonBase>();

            if (DefaultCommmands != null && DefaultCommmands.Count > 0)
                items.AddRange(DefaultCommmands);

            if (ItemsSource != null)
                items.AddRange((ObservableCollection<CommandButtonBase>)ItemsSource);
            ItemsSource = items;
        }

        #endregion Methods
    }
}