using System.Windows;
using System.Windows.Controls;

namespace My.CoachManager.Presentation.Prism.Controls
{
    public class NavigationItem : Button
    {
        /// <summary>
        /// Identifies the Target dependency property.
        /// </summary>
        public static readonly DependencyProperty TargetProperty = DependencyProperty.Register("Target", typeof(string), typeof(NavigationExpander));

        /// <summary>
        /// Gets or sets the Target of this window instance.
        /// </summary>
        public string Target
        {
            get { return (string)GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }
    }
}