using System.Windows;
using System.Windows.Media;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace My.CoachManager.Presentation.Controls.Dialogs
{
    public partial class ProgressMessageDialog
    {
        internal ProgressMessageDialog()
            : this(null)
        {
        }

        internal ProgressMessageDialog(MetroWindow parentWindow, MetroDialogSettings settings = null)
            : base(parentWindow, settings)
        {
            InitializeComponent();
        }

        protected override void OnLoaded()
        {
            SetResourceReference(ProgressBarForegroundProperty, DialogSettings.ColorScheme == MetroDialogColorScheme.Theme ? "AccentColorBrush" : "BlackBrush");
        }

        public static readonly DependencyProperty ProgressBarForegroundProperty = DependencyProperty.Register("ProgressBarForeground", typeof(Brush), typeof(ProgressDialog), new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.AffectsRender));
        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register("Message", typeof(string), typeof(ProgressDialog), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty NegativeButtonTextProperty = DependencyProperty.Register("NegativeButtonText", typeof(string), typeof(ProgressDialog), new PropertyMetadata("Cancel"));

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public Brush ProgressBarForeground
        {
            get { return (Brush)GetValue(ProgressBarForegroundProperty); }
            set { SetValue(ProgressBarForegroundProperty, value); }
        }

        internal double Minimum
        {
            get { return PART_ProgressBar.Minimum; }
            set { PART_ProgressBar.Minimum = value; }
        }

        internal double Maximum
        {
            get { return PART_ProgressBar.Maximum; }
            set { PART_ProgressBar.Maximum = value; }
        }

        internal double ProgressValue
        {
            get { return PART_ProgressBar.Value; }
            set
            {
                PART_ProgressBar.IsIndeterminate = false;
                PART_ProgressBar.Value = value;
                PART_ProgressBar.ApplyTemplate();
            }
        }

        internal void SetIndeterminate()
        {
            PART_ProgressBar.IsIndeterminate = true;
        }
    }
}