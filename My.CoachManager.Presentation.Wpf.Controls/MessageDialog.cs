using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace My.CoachManager.Presentation.Wpf.Controls
{
    [TemplatePart(Name = PartOkButton, Type = typeof(Button))]
    [TemplatePart(Name = PartCancelButton, Type = typeof(Button))]
    [TemplatePart(Name = PartYesButton, Type = typeof(Button))]
    [TemplatePart(Name = PartNoButton, Type = typeof(Button))]
    [TemplatePart(Name = PartImage, Type = typeof(Icon))]
    public class MessageDialog : DialogWindow
    {
        private const string PartOkButton = "PART_OkButton";
        private const string PartCancelButton = "PART_CancelButton";
        private const string PartYesButton = "PART_YesButton";
        private const string PartNoButton = "PART_NoButton";
        private const string PartImage = "PART_Image";
        private Button _cancelButton;
        private Icon _image;
        private Button _noButton;

        private Button _okButton;
        private MessageBoxResult _result;
        private Button _yesButton;

        static MessageDialog()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MessageDialog),
                new FrameworkPropertyMetadata(typeof(MessageDialog)));
        }

        #region Button

        /// <summary>
        ///     Identifies the <see cref="Button" /> property.
        /// </summary>
        public static readonly DependencyProperty ButtonProperty = DependencyProperty.Register("Button",
            typeof(MessageBoxButton), typeof(MessageDialog),
            new PropertyMetadata(MessageBoxButton.OK, OnButtonChanged));

        private static void OnButtonChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as MessageDialog)?.OnButtonChanged();
        }

        private void OnButtonChanged()
        {
            if (Button == MessageBoxButton.OK || Button == MessageBoxButton.OKCancel)
            {
                if (_okButton != null) FocusManager.SetFocusedElement(this, _okButton);
            }
            else if (Button == MessageBoxButton.YesNo || Button == MessageBoxButton.YesNoCancel)
            {
                if (_yesButton != null) FocusManager.SetFocusedElement(this, _yesButton);
            }
        }

        /// <summary>
        ///     Identifies the <see cref="Button" /> property.
        /// </summary>
        public MessageBoxButton Button
        {
            get => (MessageBoxButton) GetValue(ButtonProperty);
            set => SetValue(ButtonProperty, value);
        }

        #endregion

        #region Image

        /// <summary>
        ///     Identifies the <see cref="Image" /> property.
        /// </summary>
        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register("Image",
            typeof(MessageBoxImage), typeof(MessageDialog), new PropertyMetadata(MessageBoxImage.None, OnImageChanged));

        private static void OnImageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as MessageDialog)?.OnImageChanged();
        }

        private void OnImageChanged()
        {
            if (_image == null) return;
            switch (Image)
            {

                case MessageBoxImage.Hand:
                    _image.Content = Application.Current.TryFindResource("ErrorGeometry");
                    _image.Visibility = Visibility.Visible;
                    _image.Foreground = (Brush)Application.Current.TryFindResource("Negative");
                    break;
                case MessageBoxImage.Question:
                case MessageBoxImage.Asterisk:
                    _image.Content = Application.Current.TryFindResource("InformationGeometry");
                    _image.Visibility = Visibility.Visible;
                    _image.Foreground = (Brush)Application.Current.TryFindResource("Information");
                    break;
                case MessageBoxImage.Exclamation:
                    _image.Content = Application.Current.TryFindResource("WarningGeometry");
                    _image.Visibility = Visibility.Visible;
                    _image.Foreground = (Brush)Application.Current.TryFindResource("Warning");
                    break;

                default:
                    _image.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        /// <summary>
        ///     Identifies the <see cref="Image" /> property.
        /// </summary>
        public MessageBoxImage Image
        {
            get => (MessageBoxImage) GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);
        }

        #endregion

        #region DefaultResult

        /// <summary>
        ///     Identifies the <see cref="DefaultResult" /> property.
        /// </summary>
        public static readonly DependencyProperty DefaultResultProperty = DependencyProperty.Register("DefaultResult",
            typeof(MessageBoxResult), typeof(MessageDialog),
            new PropertyMetadata(MessageBoxResult.None, OnDefaultResultChanged));

        private static void OnDefaultResultChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as MessageDialog)?.OnDefaultResultChanged();
        }

        private void OnDefaultResultChanged()
        {
            _result = DefaultResult;
        }

        /// <summary>
        ///     Identifies the <see cref="DefaultResult" /> property.
        /// </summary>
        public MessageBoxResult DefaultResult
        {
            get => (MessageBoxResult) GetValue(DefaultResultProperty);
            set => SetValue(DefaultResultProperty, value);
        }

        #endregion

        #region Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _okButton = GetTemplateChild(PartOkButton) as Button;
            _cancelButton = GetTemplateChild(PartCancelButton) as Button;
            _yesButton = GetTemplateChild(PartYesButton) as Button;
            _noButton = GetTemplateChild(PartNoButton) as Button;
            _image = GetTemplateChild(PartImage) as Icon;

            if (_okButton != null) _okButton.Click += (sender, args) => { Close(MessageBoxResult.OK); };
            if (_cancelButton != null) _cancelButton.Click += (sender, args) => { Close(MessageBoxResult.Cancel); };
            if (_yesButton != null) _yesButton.Click += (sender, args) => { Close(MessageBoxResult.Yes); };
            if (_noButton != null) _noButton.Click += (sender, args) => { Close(MessageBoxResult.No); };

            OnImageChanged();
            OnButtonChanged();
            OnDefaultResultChanged();
        }

        private void Close(MessageBoxResult result)
        {
            _result = result;
            Close();
        }

        public new MessageBoxResult ShowDialog()
        {
            base.ShowDialog();
            return _result;
        }

        #endregion
    }
}