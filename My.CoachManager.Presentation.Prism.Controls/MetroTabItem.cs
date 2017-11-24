using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using My.CoachManager.Presentation.Prism.Controls.Commands;
using My.CoachManager.Presentation.Prism.Controls.Helpers;

namespace My.CoachManager.Presentation.Prism.Controls
{
    /// <summary>
    /// An extended TabItem with a metro style.
    /// </summary>
    public class MetroTabItem : TabItem
    {
        public MetroTabItem()
        {
            DefaultStyleKey = typeof(MetroTabItem);
            InternalCloseTabCommand = new CloseCommand(InternalCloseTabCommandCanExecute, InternalCloseTabCommandExecuteAction);
        }

        private void InternalCloseTabCommandExecuteAction(object o)
        {
            var closeTabCommand = CloseTabCommand;
            if (closeTabCommand != null)
            {
                var closeTabCommandParameter = CloseTabCommandParameter ?? this;
                if (closeTabCommand.CanExecute(closeTabCommandParameter))
                {
                    // force the command handler to run
                    closeTabCommand.Execute(closeTabCommandParameter);
                }
            }

            var owningTabControl = this.TryFindParent<BaseMetroTabControl>();
            // run the command handler for the TabControl
            // see #555
            owningTabControl?.BeginInvoke(() => owningTabControl.CloseThisTabItem(this));
        }

        private bool InternalCloseTabCommandCanExecute(object o)
        {
            var closeTabCommand = CloseTabCommand;
            return closeTabCommand == null || closeTabCommand.CanExecute(CloseTabCommandParameter ?? this);
        }

        public Button CloseButton;
        public Thickness NewButtonMargin;
        public ContentPresenter ContentSite;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            AdjustCloseButton();

            ContentSite = GetTemplateChild("ContentSite") as ContentPresenter;
        }

        private void AdjustCloseButton()
        {
            CloseButton = CloseButton ?? GetTemplateChild("PART_CloseButton") as Button;
            if (CloseButton != null)
            {
                CloseButton.Margin = NewButtonMargin;
            }
        }

        public static readonly DependencyProperty CloseButtonEnabledProperty =
            DependencyProperty.Register("CloseButtonEnabled",
                                        typeof(bool),
                                        typeof(MetroTabItem),
                                        new FrameworkPropertyMetadata(false,
                                                                      FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.Inherits,
                                                                      OnCloseButtonEnabledPropertyChangedCallback));

        private static void OnCloseButtonEnabledPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var item = dependencyObject as MetroTabItem;
            item?.AdjustCloseButton();
        }

        /// <summary>
        /// Gets/sets whether the Close Button is visible.
        /// </summary>
        public bool CloseButtonEnabled
        {
            get { return (bool)GetValue(CloseButtonEnabledProperty); }
            set { SetValue(CloseButtonEnabledProperty, value); }
        }

        public static readonly DependencyProperty InternalCloseTabCommandProperty =
            DependencyProperty.Register("InternalCloseTabCommand",
                                        typeof(ICommand),
                                        typeof(MetroTabItem));

        /// <summary>
        /// Gets/sets the command that is executed when the Close Button is clicked.
        /// </summary>
        public ICommand InternalCloseTabCommand
        {
            get { return (ICommand)GetValue(InternalCloseTabCommandProperty); }
            set { SetValue(InternalCloseTabCommandProperty, value); }
        }

        public static readonly DependencyProperty CloseTabCommandProperty =
            DependencyProperty.Register("CloseTabCommand",
                                        typeof(ICommand),
                                        typeof(MetroTabItem));

        /// <summary>
        /// Gets/sets the command that is executed when the Close Button is clicked.
        /// </summary>
        public ICommand CloseTabCommand
        {
            get { return (ICommand)GetValue(CloseTabCommandProperty); }
            set { SetValue(CloseTabCommandProperty, value); }
        }

        public static readonly DependencyProperty CloseTabCommandParameterProperty =
            DependencyProperty.Register("CloseTabCommandParameter",
                                        typeof(object),
                                        typeof(MetroTabItem),
                                        new PropertyMetadata(null));

        /// <summary>
        /// Gets/sets the command parameter which is passed to the close button command.
        /// </summary>
        public object CloseTabCommandParameter
        {
            get { return GetValue(CloseTabCommandParameterProperty); }
            set { SetValue(CloseTabCommandParameterProperty, value); }
        }
    }
}