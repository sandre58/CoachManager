using System.Windows;

namespace My.CoachManager.Presentation.Parameters
{
    public static class FormParameters
    {
        #region Label

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.RegisterAttached("Label", typeof(string), typeof(FormParameters),
                                                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender |
                                                                                    FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        [AttachedPropertyBrowsableForType(typeof(UIElement))]
        public static string GetLabel(UIElement obj)
        {
            return (string)obj.GetValue(LabelProperty);
        }

        public static void SetLabel(UIElement obj, string value)
        {
            obj.SetValue(LabelProperty, value);
        }

        #endregion Label

        #region IsRequired

        public static readonly DependencyProperty IsRequiredProperty =
            DependencyProperty.RegisterAttached("IsRequired", typeof(bool), typeof(FormParameters),
                                                new FrameworkPropertyMetadata(null));

        [AttachedPropertyBrowsableForType(typeof(UIElement))]
        public static bool GetIsRequired(UIElement obj)
        {
            return (bool)obj.GetValue(IsRequiredProperty);
        }

        public static void SetIsRequired(UIElement obj, string value)
        {
            obj.SetValue(IsRequiredProperty, value);
        }

        #endregion IsRequired

        #region Icon

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.RegisterAttached("Icon", typeof(Style), typeof(FormParameters),
                                                new FrameworkPropertyMetadata(null));

        [AttachedPropertyBrowsableForType(typeof(UIElement))]
        public static Style GetIcon(UIElement obj)
        {
            return (Style)obj.GetValue(IconProperty);
        }

        public static void SetIcon(UIElement obj, Style value)
        {
            obj.SetValue(LabelProperty, value);
        }

        #endregion Icon
    }
}