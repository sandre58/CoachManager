using My.CoachManager.Presentation.Controls.Helpers;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls.Primitives;
using My.CoachManager.Presentation.Controls.Buttons;

namespace My.CoachManager.Presentation.Controls.Parameters
{
    /// <summary>
    /// A helper class that provides various controls.
    /// </summary>
    public static class ButtonParameters
    {
        #region IconPlacement

        public static readonly DependencyProperty ButtonAlignmentProperty = DependencyProperty.RegisterAttached("ButtonAlignment", typeof(ButtonsAlignment), typeof(ButtonParameters), new FrameworkPropertyMetadata(ButtonsAlignment.Left, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Gets the character casing of the control
        /// </summary>
        [Category(Constants.ParameterCategory)]
        [AttachedPropertyBrowsableForType(typeof(ButtonBase))]
        public static ButtonsAlignment GetButtonAlignment(ButtonBase element)
        {
            var value = element.GetValue(ButtonAlignmentProperty);
            if (value != null)
                return (ButtonsAlignment)value;
            return ButtonsAlignment.Left;
        }

        /// <summary>
        /// Sets the character casing of the control
        /// </summary>
        public static void SetButtonAlignment(ButtonBase element, ButtonsAlignment value)
        {
            element.SetValue(ButtonAlignmentProperty, value);
        }

        #endregion CharacterCasing

    }
}