using System.Windows;
using System.Windows.Controls;
using My.CoachManager.CrossCutting.Core.Enums;

namespace My.CoachManager.Presentation.Prism.Controls
{
    public class InjuryTypeSelector : ContentControl
    {
        /// <summary>
        /// Identifies the BackgroundContent dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectedTypeProperty = DependencyProperty.Register("SelectedType", typeof(InjuryType), typeof(InjuryTypeSelector));

        /// <summary>
        /// Gets or sets the background content of this window instance.
        /// </summary>
        public InjuryType SelectedType
        {
            get => (InjuryType)GetValue(SelectedTypeProperty);
            set => SetValue(SelectedTypeProperty, value);
        }
    }
}
