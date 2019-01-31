using System.Windows;
using System.Windows.Media;

namespace My.CoachManager.Presentation.Controls
{
    public class Icon : System.Windows.Controls.ContentControl
    {
        #region Dependency Properties

        private static readonly DependencyPropertyKey IsOverlayVisiblePropertyKey = DependencyProperty.RegisterReadOnly(
            "IsOverlayVisible",
            typeof(bool),
            typeof(Icon),
            new PropertyMetadata(false));

        public static readonly DependencyProperty IsOverlayVisibleProperty = IsOverlayVisiblePropertyKey.DependencyProperty;

        public static readonly DependencyProperty OverlayBackgroundProperty = DependencyProperty.Register(
            "OverlayBackground",
            typeof(Brush),
            typeof(Icon),
            new PropertyMetadata(null));

        public static readonly DependencyProperty OverlayBorderBrushProperty = DependencyProperty.Register(
            "OverlayBorderBrush",
            typeof(Brush),
            typeof(Icon),
            new PropertyMetadata(null));

        public static readonly DependencyProperty OverlayBorderThicknessProperty = DependencyProperty.Register(
            "OverlayBorderThickness",
            typeof(Thickness),
            typeof(Icon),
            new PropertyMetadata(new Thickness()));

        private static readonly DependencyPropertyKey OverlayHeightPropertyKey = DependencyProperty.RegisterReadOnly(
            "OverlayHeight",
            typeof(double),
            typeof(Icon),
            new PropertyMetadata(0D));

        public static readonly DependencyProperty OverlayHeightProperty = OverlayHeightPropertyKey.DependencyProperty;

        public static readonly DependencyProperty OverlayHorizontalAlignmentProperty = DependencyProperty.Register(
            "OverlayHorizontalAlignment",
            typeof(HorizontalAlignment),
            typeof(Icon),
            new PropertyMetadata(HorizontalAlignment.Right));

        public static readonly DependencyProperty OverlayMarginProperty = DependencyProperty.Register(
            "OverlayMargin",
            typeof(Thickness),
            typeof(Icon),
            new PropertyMetadata(new Thickness()));

        public static readonly DependencyProperty OverlayPaddingProperty = DependencyProperty.Register(
            "OverlayPadding",
            typeof(Thickness),
            typeof(Icon),
            new PropertyMetadata(new Thickness()));

        public static readonly DependencyProperty OverlayStyleProperty = DependencyProperty.Register(
            "OverlayStyle",
            typeof(Style),
            typeof(Icon),
            new PropertyMetadata(null, OnOverlayStylePropertyChanged));

        public static readonly DependencyProperty OverlayVerticalAlignmentProperty = DependencyProperty.Register(
            "OverlayVerticalAlignment",
            typeof(VerticalAlignment),
            typeof(Icon),
            new PropertyMetadata(VerticalAlignment.Bottom));

        private static readonly DependencyPropertyKey OverlayWidthPropertyKey = DependencyProperty.RegisterReadOnly(
            "OverlayWidth",
            typeof(double),
            typeof(Icon),
            new PropertyMetadata(0D));

        public static readonly DependencyProperty OverlayWidthProperty = OverlayWidthPropertyKey.DependencyProperty;

        public static readonly DependencyProperty SizeProperty = DependencyProperty.Register(
            "Size",
            typeof(IconSize),
            typeof(Icon),
            new PropertyMetadata(IconSize.Small, OnSizePropertyChanged));

        public static readonly DependencyProperty StretchProperty = DependencyProperty.Register(
            "Stretch",
            typeof(Stretch),
            typeof(Icon),
            new PropertyMetadata(Stretch.Uniform));

        #endregion Dependency Properties

        #region Constructors

        /// <summary>
        /// Initialises static members of the <see cref="Icon"/> class.
        /// </summary>
        static Icon()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(Icon),
                new FrameworkPropertyMetadata(typeof(Icon)));
            HeightProperty.OverrideMetadata(
                typeof(Icon),
                new FrameworkPropertyMetadata(OnHeightPropertyChanged));
            WidthProperty.OverrideMetadata(
                typeof(Icon),
                new FrameworkPropertyMetadata(OnWidthPropertyChanged));
        }

        #endregion Constructors

        #region Public Properties

        public bool IsOverlayVisible
        {
            get
            {
                var value = GetValue(IsOverlayVisibleProperty);
                return value != null && (bool)value;
            }
            set { SetValue(IsOverlayVisiblePropertyKey, value); }
        }

        public Brush OverlayBackground
        {
            get { return (Brush)GetValue(OverlayBackgroundProperty); }
            set { SetValue(OverlayBackgroundProperty, value); }
        }

        public Brush OverlayBorderBrush
        {
            get { return (Brush)GetValue(OverlayBorderBrushProperty); }
            set { SetValue(OverlayBorderBrushProperty, value); }
        }

        public Thickness OverlayBorderThickness
        {
            get
            {
                var value = GetValue(OverlayBorderThicknessProperty);
                if (value != null)
                    return (Thickness)value;
                return new Thickness();
            }
            set { SetValue(OverlayBorderThicknessProperty, value); }
        }

        public double OverlayHeight
        {
            get
            {
                var value = GetValue(OverlayHeightProperty);
                if (value != null) return (double)value;
                return 0;
            }
            private set { SetValue(OverlayHeightPropertyKey, value); }
        }

        public HorizontalAlignment OverlayHorizontalAlignment
        {
            get
            {
                var value = GetValue(OverlayHorizontalAlignmentProperty);
                if (value != null)
                    return (HorizontalAlignment)value;
                return default(HorizontalAlignment);
            }
            set { SetValue(OverlayHorizontalAlignmentProperty, value); }
        }

        public Thickness OverlayMargin
        {
            get
            {
                var value = GetValue(OverlayMarginProperty);
                if (value != null) return (Thickness)value;
                return new Thickness();
            }
            set { SetValue(OverlayMarginProperty, value); }
        }

        public Thickness OverlayPadding
        {
            get
            {
                var value = GetValue(OverlayPaddingProperty);
                if (value != null) return (Thickness)value;
                return new Thickness();
            }
            set { SetValue(OverlayPaddingProperty, value); }
        }

        public Style OverlayStyle
        {
            get { return (Style)GetValue(OverlayStyleProperty); }
            set { SetValue(OverlayStyleProperty, value); }
        }

        public VerticalAlignment OverlayVerticalAlignment
        {
            get
            {
                var value = GetValue(OverlayVerticalAlignmentProperty);
                if (value != null)
                    return (VerticalAlignment)value;
                return default(VerticalAlignment);
            }
            set { SetValue(OverlayVerticalAlignmentProperty, value); }
        }

        public double OverlayWidth
        {
            get
            {
                var value = GetValue(OverlayWidthProperty);
                if (value != null) return (double)value;
                return 0;
            }
            private set { SetValue(OverlayWidthPropertyKey, value); }
        }

        public IconSize Size
        {
            get
            {
                var value = GetValue(SizeProperty);
                if (value != null) return (IconSize)value;
                return default(IconSize);
            }
            set { SetValue(SizeProperty, value); }
        }

        public Stretch Stretch
        {
            get
            {
                var value = GetValue(StretchProperty);
                if (value != null) return (Stretch)value;
                return default(Stretch);
            }
            set { SetValue(StretchProperty, value); }
        }

        #endregion Public Properties

        #region Private Static Methods

        private static void OnHeightPropertyChanged(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs e)
        {
            Icon icon = (Icon)dependencyObject;
            icon.OverlayHeight = (icon.Height / 4D) + 4D;
        }

        private static void OnOverlayStylePropertyChanged(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs e)
        {
            Icon icon = (Icon)dependencyObject;
            icon.UpdateIsOverlayVisible();
        }

        private static void OnSizePropertyChanged(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs e)
        {
            Icon icon = (Icon)dependencyObject;
            icon.UpdateIsOverlayVisible();
        }

        private static void OnWidthPropertyChanged(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs e)
        {
            Icon icon = (Icon)dependencyObject;
            icon.OverlayWidth = (icon.Width / 4D) + 4D;
        }

        #endregion Private Static Methods

        #region Private Methods

        private void UpdateIsOverlayVisible()
        {
            IsOverlayVisible = (OverlayStyle != null) &&
                ((Size == IconSize.Medium) ||
                (Size == IconSize.Large) ||
                (Size == IconSize.VeryLarge) ||
                (Size == IconSize.Custom));
        }

        #endregion Private Methods
    }

    public enum IconSize
    {
        Small,
        Medium,
        Large,
        VeryLarge,
        Custom
    }
}