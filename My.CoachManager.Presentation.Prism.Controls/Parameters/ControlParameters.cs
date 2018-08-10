using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using My.CoachManager.Presentation.Prism.Controls.ContentControls;
using My.CoachManager.Presentation.Prism.Controls.Helpers;

namespace My.CoachManager.Presentation.Prism.Controls.Parameters
{
    /// <summary>
    /// A helper class that provides various controls.
    /// </summary>
    public static class ControlParameters
    {
        #region ContentCharacterCasing

        /// <summary>
        /// The DependencyProperty for the CharacterCasing property.
        /// Controls whether or not content is converted to upper or lower case
        /// </summary>
        public static readonly DependencyProperty ContentCharacterCasingProperty =
            DependencyProperty.RegisterAttached(
                "ContentCharacterCasing",
                typeof(CharacterCase),
                typeof(ControlParameters),
                new FrameworkPropertyMetadata(CharacterCase.Normal, FrameworkPropertyMetadataOptions.AffectsMeasure),
                value => CharacterCase.Normal <= (CharacterCase)value && (CharacterCase)value <= CharacterCase.FirstLetterUpper);

        /// <summary>
        /// Gets the character casing of the control
        /// </summary>
        [Category(Constants.ParameterCategory)]
        [AttachedPropertyBrowsableForType(typeof(UIElement))]
        public static CharacterCase GetContentCharacterCasing(UIElement element)
        {
            var value = element.GetValue(ContentCharacterCasingProperty);
            if (value != null)
                return (CharacterCase)value;
            return CharacterCase.Normal;
        }

        /// <summary>
        /// Sets the character casing of the control
        /// </summary>
        public static void SetContentCharacterCasing(UIElement element, CharacterCase value)
        {
            element.SetValue(ContentCharacterCasingProperty, value);
        }

        #endregion ContentCharacterCasing

        #region Icon

        public static readonly DependencyProperty IconProperty =
    DependencyProperty.RegisterAttached("Icon", typeof(object), typeof(ControlParameters));

        [AttachedPropertyBrowsableForType(typeof(FrameworkElement))]
        public static object GetIcon(FrameworkElement obj)
        {
            return obj.GetValue(IconProperty);
        }

        public static void SetIcon(FrameworkElement obj, object value)
        {
            obj.SetValue(IconProperty, value);
        }

        #endregion Icon

        #region Color

        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.RegisterAttached("Color", typeof(SolidColorBrush), typeof(ControlParameters));

        [AttachedPropertyBrowsableForType(typeof(FrameworkElement))]
        public static SolidColorBrush GetColor(FrameworkElement obj)
        {
            return (SolidColorBrush)obj.GetValue(IconProperty);
        }

        public static void SetColor(FrameworkElement obj, SolidColorBrush value)
        {
            obj.SetValue(IconProperty, value);
        }

        #endregion Color

        #region Foreground

        public static readonly DependencyProperty ForegroundProperty =
    DependencyProperty.RegisterAttached("Foreground", typeof(SolidColorBrush), typeof(ControlParameters),
                                        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender |
                                                                            FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        [AttachedPropertyBrowsableForType(typeof(FrameworkElement))]
        public static SolidColorBrush GetForeground(FrameworkElement obj)
        {
            return (SolidColorBrush)obj.GetValue(ForegroundProperty);
        }

        public static void SetForeground(FrameworkElement obj, SolidColorBrush value)
        {
            obj.SetValue(ForegroundProperty, value);
        }

        #endregion Foreground

        #region Background

        public static readonly DependencyProperty BackgroundProperty =
    DependencyProperty.RegisterAttached("Background", typeof(SolidColorBrush), typeof(ControlParameters),
                                        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender |
                                                                            FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        [AttachedPropertyBrowsableForType(typeof(FrameworkElement))]
        public static SolidColorBrush GetBackground(FrameworkElement obj)
        {
            return (SolidColorBrush)obj.GetValue(BackgroundProperty);
        }

        public static void SetBackground(FrameworkElement obj, SolidColorBrush value)
        {
            obj.SetValue(BackgroundProperty, value);
        }

        #endregion Background

        #region BorderBrush

        public static readonly DependencyProperty BorderBrushProperty =
    DependencyProperty.RegisterAttached("BorderBrush", typeof(SolidColorBrush), typeof(ControlParameters),
                                        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender |
                                                                            FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        [AttachedPropertyBrowsableForType(typeof(FrameworkElement))]
        public static SolidColorBrush GetBorderBrush(FrameworkElement obj)
        {
            return (SolidColorBrush)obj.GetValue(BorderBrushProperty);
        }

        public static void SetBorderBrush(FrameworkElement obj, SolidColorBrush value)
        {
            obj.SetValue(BorderBrushProperty, value);
        }

        #endregion BorderBrush

        #region MouseOverForeground

        public static readonly DependencyProperty MouseOverForegroundProperty =
    DependencyProperty.RegisterAttached("MouseOverForeground", typeof(SolidColorBrush), typeof(ControlParameters),
                                        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender |
                                                                            FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        [AttachedPropertyBrowsableForType(typeof(FrameworkElement))]
        public static SolidColorBrush GetMouseOverForeground(FrameworkElement obj)
        {
            return (SolidColorBrush)obj.GetValue(MouseOverForegroundProperty);
        }

        public static void SetMouseOverForeground(FrameworkElement obj, SolidColorBrush value)
        {
            obj.SetValue(MouseOverForegroundProperty, value);
        }

        #endregion MouseOverForeground

        #region MouseOverBackground

        public static readonly DependencyProperty MouseOverBackgroundProperty =
    DependencyProperty.RegisterAttached("MouseOverBackground", typeof(SolidColorBrush), typeof(ControlParameters),
                                        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender |
                                                                            FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        [AttachedPropertyBrowsableForType(typeof(FrameworkElement))]
        public static SolidColorBrush GetMouseOverBackground(FrameworkElement obj)
        {
            return (SolidColorBrush)obj.GetValue(MouseOverBackgroundProperty);
        }

        public static void SetMouseOverBackground(FrameworkElement obj, SolidColorBrush value)
        {
            obj.SetValue(MouseOverBackgroundProperty, value);
        }

        #endregion MouseOverBackground

        #region MouseOverBorderBrush

        public static readonly DependencyProperty MouseOverBorderBrushProperty =
    DependencyProperty.RegisterAttached("MouseOverBorderBrush", typeof(SolidColorBrush), typeof(ControlParameters),
                                        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender |
                                                                            FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        [AttachedPropertyBrowsableForType(typeof(FrameworkElement))]
        public static SolidColorBrush GetMouseOverBorderBrush(FrameworkElement obj)
        {
            return (SolidColorBrush)obj.GetValue(MouseOverBorderBrushProperty);
        }

        public static void SetMouseOverBorderBrush(FrameworkElement obj, SolidColorBrush value)
        {
            obj.SetValue(MouseOverBorderBrushProperty, value);
        }

        #endregion MouseOverBorderBrush

        #region PressedForeground

        public static readonly DependencyProperty PressedForegroundProperty =
    DependencyProperty.RegisterAttached("PressedForeground", typeof(SolidColorBrush), typeof(ControlParameters),
                                        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender |
                                                                            FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        [AttachedPropertyBrowsableForType(typeof(FrameworkElement))]
        public static SolidColorBrush GetPressedForeground(FrameworkElement obj)
        {
            return (SolidColorBrush)obj.GetValue(PressedForegroundProperty);
        }

        public static void SetPressedForeground(FrameworkElement obj, SolidColorBrush value)
        {
            obj.SetValue(PressedForegroundProperty, value);
        }

        #endregion PressedForeground

        #region PressedBackground

        public static readonly DependencyProperty PressedBackgroundProperty =
    DependencyProperty.RegisterAttached("PressedBackground", typeof(SolidColorBrush), typeof(ControlParameters),
                                        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender |
                                                                            FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        [AttachedPropertyBrowsableForType(typeof(FrameworkElement))]
        public static SolidColorBrush GetPressedBackground(FrameworkElement obj)
        {
            return (SolidColorBrush)obj.GetValue(PressedBackgroundProperty);
        }

        public static void SetPressedBackground(FrameworkElement obj, SolidColorBrush value)
        {
            obj.SetValue(PressedBackgroundProperty, value);
        }

        #endregion PressedBackground

        #region PressedBorderBrush

        public static readonly DependencyProperty PressedBorderBrushProperty =
    DependencyProperty.RegisterAttached("PressedBorderBrush", typeof(SolidColorBrush), typeof(ControlParameters),
                                        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender |
                                                                            FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        [AttachedPropertyBrowsableForType(typeof(FrameworkElement))]
        public static SolidColorBrush GetPressedBorderBrush(FrameworkElement obj)
        {
            return (SolidColorBrush)obj.GetValue(PressedBorderBrushProperty);
        }

        public static void SetPressedBorderBrush(FrameworkElement obj, SolidColorBrush value)
        {
            obj.SetValue(PressedBorderBrushProperty, value);
        }

        #endregion PressedBorderBrush

        #region FocusForeground

        public static readonly DependencyProperty FocusForegroundProperty =
    DependencyProperty.RegisterAttached("FocusForeground", typeof(SolidColorBrush), typeof(ControlParameters),
                                        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender |
                                                                            FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        [AttachedPropertyBrowsableForType(typeof(FrameworkElement))]
        public static SolidColorBrush GetFocusForeground(FrameworkElement obj)
        {
            return (SolidColorBrush)obj.GetValue(FocusForegroundProperty);
        }

        public static void SetFocusForeground(FrameworkElement obj, SolidColorBrush value)
        {
            obj.SetValue(FocusForegroundProperty, value);
        }

        #endregion FocusForeground

        #region FocusBackground

        public static readonly DependencyProperty FocusBackgroundProperty =
    DependencyProperty.RegisterAttached("FocusBackground", typeof(SolidColorBrush), typeof(ControlParameters),
                                        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender |
                                                                            FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        [AttachedPropertyBrowsableForType(typeof(FrameworkElement))]
        public static SolidColorBrush GetFocusBackground(FrameworkElement obj)
        {
            return (SolidColorBrush)obj.GetValue(FocusBackgroundProperty);
        }

        public static void SetFocusBackground(FrameworkElement obj, SolidColorBrush value)
        {
            obj.SetValue(FocusBackgroundProperty, value);
        }

        #endregion FocusBackground

        #region FocusBorderBrush

        public static readonly DependencyProperty FocusBorderBrushProperty =
    DependencyProperty.RegisterAttached("FocusBorderBrush", typeof(SolidColorBrush), typeof(ControlParameters),
                                        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender |
                                                                            FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        [AttachedPropertyBrowsableForType(typeof(FrameworkElement))]
        public static SolidColorBrush GetFocusBorderBrush(FrameworkElement obj)
        {
            return (SolidColorBrush)obj.GetValue(FocusBorderBrushProperty);
        }

        public static void SetFocusBorderBrush(FrameworkElement obj, SolidColorBrush value)
        {
            obj.SetValue(FocusBorderBrushProperty, value);
        }

        #endregion FocusBorderBrush

        #region CornerRadius

        /// <summary>
        /// DependencyProperty for <see cref="CornerRadius" /> property.
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty
            = DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(ControlParameters),
                                                  new FrameworkPropertyMetadata(
                                                      new CornerRadius(),
                                                      FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// The CornerRadius property allows users to control the roundness of the button corners independently by
        /// setting a radius value for each corner. Radius values that are too large are scaled so that they
        /// smoothly blend from corner to corner. (Can be used e.g. at MetroButton style)
        /// Description taken from original Microsoft description :-D
        /// </summary>
        [Category(Constants.ParameterCategory)]
        public static CornerRadius GetCornerRadius(UIElement element)
        {
            var value = element.GetValue(CornerRadiusProperty);
            if (value != null)
                return (CornerRadius)value;
            return new CornerRadius();
        }

        public static void SetCornerRadius(UIElement element, CornerRadius value)
        {
            element.SetValue(CornerRadiusProperty, value);
        }

        #endregion CornerRadius

        #region Header

        public static readonly DependencyProperty HeaderProperty
            = DependencyProperty.RegisterAttached("Header", typeof(object), typeof(ControlParameters));

        /// <summary>
        /// The CornerRadius property allows users to control the roundness of the button corners independently by
        /// setting a radius value for each corner. Radius values that are too large are scaled so that they
        /// smoothly blend from corner to corner. (Can be used e.g. at MetroButton style)
        /// Description taken from original Microsoft description :-D
        /// </summary>
        [Category(Constants.ParameterCategory)]
        public static object GetHeader(UIElement element)
        {
            return element.GetValue(HeaderProperty);
        }

        public static void SetHeader(UIElement element, object value)
        {
            element.SetValue(HeaderProperty, value);
        }

        #endregion Header

        #region HeaderTemplate

        public static readonly DependencyProperty HeaderTemplateProperty
            = DependencyProperty.RegisterAttached("HeaderTemplate", typeof(FrameworkTemplate), typeof(ControlParameters));

        /// <summary>
        /// The CornerRadius property allows users to control the roundness of the button corners independently by
        /// setting a radius value for each corner. Radius values that are too large are scaled so that they
        /// smoothly blend from corner to corner. (Can be used e.g. at MetroButton style)
        /// Description taken from original Microsoft description :-D
        /// </summary>
        [Category(Constants.ParameterCategory)]
        public static FrameworkTemplate GetHeaderTemplate(UIElement element)
        {
            return (FrameworkTemplate)element.GetValue(HeaderTemplateProperty);
        }

        public static void SetHeaderTemplate(UIElement element, FrameworkTemplate value)
        {
            element.SetValue(HeaderTemplateProperty, value);
        }

        #endregion HeaderTemplate

        #region DisabledVisualElementVisibility

        public static readonly DependencyProperty DisabledVisualElementVisibilityProperty = DependencyProperty.RegisterAttached("DisabledVisualElementVisibility", typeof(Visibility), typeof(ControlParameters), new FrameworkPropertyMetadata(Visibility.Visible, FrameworkPropertyMetadataOptions.Inherits | FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Gets the value to handle the visibility of the DisabledVisualElement in the template.
        /// </summary>
        [Category(Constants.ParameterCategory)]
        [AttachedPropertyBrowsableForType(typeof(TextBoxBase))]
        [AttachedPropertyBrowsableForType(typeof(PasswordBox))]
        [AttachedPropertyBrowsableForType(typeof(ExtendedNumericUpDown))]
        public static Visibility GetDisabledVisualElementVisibility(UIElement element)
        {
            var value = element.GetValue(DisabledVisualElementVisibilityProperty);
            if (value != null)
                return (Visibility)value;
            return default(Visibility);
        }

        /// <summary>
        /// Sets the value to handle the visibility of the DisabledVisualElement in the template.
        /// </summary>
        public static void SetDisabledVisualElementVisibility(UIElement element, Visibility value)
        {
            element.SetValue(DisabledVisualElementVisibilityProperty, value);
        }

        #endregion DisabledVisualElementVisibility
    }
}