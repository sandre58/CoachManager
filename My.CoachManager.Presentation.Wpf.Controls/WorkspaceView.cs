using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace My.CoachManager.Presentation.Wpf.Controls
{
    public class WorkspaceView : Page
    {

        static WorkspaceView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WorkspaceView), new FrameworkPropertyMetadata(typeof(WorkspaceView)));
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:My.CoachManager.Presentation.Wpf.Controls.WorkspaceView" /> class.
        /// </summary>
        public WorkspaceView()
        {
            Commands = new List<FrameworkElement>();
        }

        #region Commands

        /// <summary>
        /// Identifies the <see cref="Commands"/> property.
        /// </summary>
        public static readonly DependencyProperty CommandsProperty = DependencyProperty.Register("Commands", typeof(List<FrameworkElement>), typeof(WorkspaceView), new PropertyMetadata(new List<FrameworkElement>()));

        /// <summary>
        /// Identifies the <see cref="Commands"/> property.
        /// </summary>
        public List<FrameworkElement> Commands
        {
            get => (List<FrameworkElement>)GetValue(CommandsProperty);
            set => SetValue(CommandsProperty, value);
        }

        #endregion

        #region IsLoading

        /// <summary>
        /// Identifies the <see cref="IsLoading"/> property.
        /// </summary>
        public static readonly DependencyProperty IsLoadingProperty = DependencyProperty.Register("IsLoading", typeof(bool), typeof(WorkspaceView), new PropertyMetadata(false));

        /// <summary>
        /// Identifies the <see cref="IsLoading"/> property.
        /// </summary>
        public bool IsLoading
        {
            get => (bool)GetValue(IsLoadingProperty);
            set => SetValue(IsLoadingProperty, value);
        }

        #endregion

        #region IsLoadingText

        /// <summary>
        /// Identifies the <see cref="IsLoadingText"/> property.
        /// </summary>
        public static readonly DependencyProperty IsLoadingTextProperty = DependencyProperty.Register("IsLoadingText", typeof(string), typeof(WorkspaceView));

        /// <summary>
        /// Identifies the <see cref="IsLoadingText"/> property.
        /// </summary>
        public string IsLoadingText
        {
            get => (string)GetValue(IsLoadingTextProperty);
            set => SetValue(IsLoadingTextProperty, value);
        }

        #endregion

        #region TopContentBackground

        /// <summary>
        /// Identifies the <see cref="TopContentBackground"/> property.
        /// </summary>
        public static readonly DependencyProperty TopContentBackgroundProperty = DependencyProperty.Register("TopContentBackground", typeof(Brush), typeof(WorkspaceView));

        /// <summary>
        /// Identifies the <see cref="TopContentBackground"/> property.
        /// </summary>
        public Brush TopContentBackground
        {
            get => (Brush)GetValue(TopContentBackgroundProperty);
            set => SetValue(TopContentBackgroundProperty, value);
        }

        #endregion

        #region Header

        /// <summary>
        /// Identifies the <see cref="Header"/> property.
        /// </summary>
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register("Header", typeof(object), typeof(WorkspaceView));

        /// <summary>
        /// Identifies the <see cref="Header"/> property.
        /// </summary>
        public object Header
        {
            get => GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        #endregion

        #region HeaderControlTemplate

        /// <summary>
        /// Identifies the <see cref="HeaderControlTemplate"/> property.
        /// </summary>
        public static readonly DependencyProperty HeaderControlTemplateProperty = DependencyProperty.Register("HeaderControlTemplate", typeof(ControlTemplate), typeof(WorkspaceView));

        /// <summary>
        /// Identifies the <see cref="HeaderControlTemplate"/> property.
        /// </summary>
        public ControlTemplate HeaderControlTemplate
        {
            get => (ControlTemplate)GetValue(HeaderControlTemplateProperty);
            set => SetValue(HeaderControlTemplateProperty, value);
        }

        #endregion

        #region HeaderTemplate

        /// <summary>
        /// Identifies the <see cref="HeaderTemplate"/> property.
        /// </summary>
        public static readonly DependencyProperty HeaderTemplateProperty = DependencyProperty.Register("HeaderTemplate", typeof(DataTemplate), typeof(WorkspaceView));

        /// <summary>
        /// Identifies the <see cref="HeaderTemplate"/> property.
        /// </summary>
        public DataTemplate HeaderTemplate
        {
            get => (DataTemplate)GetValue(HeaderTemplateProperty);
            set => SetValue(HeaderTemplateProperty, value);
        }

        #endregion

        #region HeaderTemplateSelector

        /// <summary>
        /// Identifies the <see cref="HeaderTemplateSelector"/> property.
        /// </summary>
        public static readonly DependencyProperty HeaderTemplateSelectorProperty = DependencyProperty.Register("HeaderTemplateSelector", typeof(DataTemplateSelector), typeof(WorkspaceView));

        /// <summary>
        /// Identifies the <see cref="HeaderTemplateSelector"/> property.
        /// </summary>
        public DataTemplateSelector HeaderTemplateSelector
        {
            get => (DataTemplateSelector)GetValue(HeaderTemplateSelectorProperty);
            set => SetValue(HeaderTemplateSelectorProperty, value);
        }

        #endregion

        #region HeaderVisibility

        /// <summary>
        /// Identifies the <see cref="HeaderVisibility"/> property.
        /// </summary>
        public static readonly DependencyProperty HeaderVisibilityProperty = DependencyProperty.Register("HeaderVisibility", typeof(Visibility), typeof(WorkspaceView), new PropertyMetadata(System.Windows.Visibility.Visible));

        /// <summary>
        /// Identifies the <see cref="HeaderVisibility"/> property.
        /// </summary>
        public Visibility HeaderVisibility
        {
            get => (Visibility)GetValue(HeaderVisibilityProperty);
            set => SetValue(HeaderVisibilityProperty, value);
        }

        #endregion

        #region TopContentTemplate

        /// <summary>
        /// Identifies the <see cref="TopContentTemplate"/> property.
        /// </summary>
        public static readonly DependencyProperty TopContentTemplateProperty = DependencyProperty.Register("TopContentTemplate", typeof(DataTemplate), typeof(WorkspaceView));

        /// <summary>
        /// Identifies the <see cref="TopContentTemplate"/> property.
        /// </summary>
        public DataTemplate TopContentTemplate
        {
            get => (DataTemplate)GetValue(TopContentTemplateProperty);
            set => SetValue(TopContentTemplateProperty, value);
        }

        #endregion

        #region TopContentTemplateSelector

        /// <summary>
        /// Identifies the <see cref="TopContentTemplateSelector"/> property.
        /// </summary>
        public static readonly DependencyProperty TopContentTemplateSelectorProperty = DependencyProperty.Register("TopContentTemplateSelector", typeof(DataTemplateSelector), typeof(WorkspaceView));

        /// <summary>
        /// Identifies the <see cref="TopContentTemplateSelector"/> property.
        /// </summary>
        public DataTemplateSelector TopContentTemplateSelector
        {
            get => (DataTemplateSelector)GetValue(TopContentTemplateSelectorProperty);
            set => SetValue(TopContentTemplateSelectorProperty, value);
        }

        #endregion
    }
}