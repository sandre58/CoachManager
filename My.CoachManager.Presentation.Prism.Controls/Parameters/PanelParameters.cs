namespace My.CoachManager.Presentation.Prism.Controls.Parameters
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// <see cref="Panel"/> attached properties.
    /// </summary>
    public static class PanelParameters
    {
        #region Dependency Properties

        public static readonly DependencyProperty ItemsMarginProperty = DependencyProperty.RegisterAttached(
            "ItemsMargin",
            typeof(Thickness),
            typeof(PanelParameters),
            new UIPropertyMetadata(new Thickness(), MarginChangedCallback));

        public static readonly DependencyProperty NotAffectMarginProperty = DependencyProperty.RegisterAttached(
    "NotAffectMargin",
    typeof(bool),
    typeof(PanelParameters),
    new UIPropertyMetadata(false));

        #endregion Dependency Properties

        #region Public Static Methods

        public static Thickness GetItemsMargin(DependencyObject obj)
        {
            var value = obj.GetValue(ItemsMarginProperty);
            if (value != null) return (Thickness)value;
            return new Thickness();
        }

        public static void SetItemsMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(ItemsMarginProperty, value);
        }

        public static bool GetNotAffectMargin(DependencyObject obj)
        {
            return (bool)obj.GetValue(NotAffectMarginProperty);
        }

        public static void SetNotAffectMargin(DependencyObject obj, bool value)
        {
            obj.SetValue(NotAffectMarginProperty, value);
        }

        public static void MarginChangedCallback(object sender, DependencyPropertyChangedEventArgs e)
        {
            // Make sure this is put on a panel
            var panel = sender as Panel;
            if (panel == null) return;

            panel.Loaded += panel_Loaded;
        }

        private static void panel_Loaded(object sender, RoutedEventArgs e)
        {
            var panel = sender as Panel;
            SetChildsMargin(panel, GetItemsMargin(panel));
        }

        private static void SetChildsMargin(Panel panel, Thickness margin)
        {
            // Go over the children and set margin for them:
            if (panel != null)
                foreach (var child in panel.Children)
                {
                    var panel1 = child as Panel;
                    if (panel1 != null)
                    {
                        SetChildsMargin(panel1, margin);
                    }
                    else
                    {
                        var fe = child as FrameworkElement;

                        if (fe == null) continue;

                        if (!GetNotAffectMargin(fe)) fe.Margin = margin;
                    }
                }
        }

        #endregion Public Static Methods
    }
}