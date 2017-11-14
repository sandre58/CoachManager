﻿using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace My.CoachManager.Presentation.Prism.Controls
{
    public class WorkspaceView : UserControl
    {
        /// <summary>
        /// Initialise une nouvelle instance de <see cref="WorkspaceView"/>.
        /// </summary>
        public WorkspaceView()
        {
            Commands = new ObservableCollection<CommandButton>();
        }

        /// <summary>
        /// Identifies the BackgroundContent dependency property.
        /// </summary>
        public static readonly DependencyProperty CommandsProperty = DependencyProperty.Register("Commands", typeof(ObservableCollection<CommandButton>), typeof(WorkspaceView));

        /// <summary>
        /// Identifies the BackgroundContent dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderTemplateProperty = DependencyProperty.Register("HeaderTemplate", typeof(DataTemplate), typeof(WorkspaceView));

        /// <summary>
        /// Identifies the BackgroundContent dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderBackgroundProperty = DependencyProperty.Register("HeaderBackground", typeof(SolidColorBrush), typeof(WorkspaceView));

        /// <summary>
        /// Identifies the BackgroundContent dependency property.
        /// </summary>
        public static readonly DependencyProperty ApplicationBarStyleProperty = DependencyProperty.Register("ApplicationBarStyle", typeof(Style), typeof(WorkspaceView));

        /// <summary>
        /// Identifies the BackgroundContent dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderHeightProperty = DependencyProperty.Register("HeaderHeight", typeof(int), typeof(WorkspaceView));

        /// <summary>
        /// Gets or sets the background content of this window instance.
        /// </summary>
        public DataTemplate HeaderTemplate
        {
            get { return (DataTemplate)GetValue(HeaderTemplateProperty); }
            set { SetValue(HeaderTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the background content of this window instance.
        /// </summary>
        public SolidColorBrush HeaderBackground
        {
            get { return (SolidColorBrush)GetValue(HeaderBackgroundProperty); }
            set { SetValue(HeaderBackgroundProperty, value); }
        }

        /// <summary>
        /// Gets or sets the background content of this window instance.
        /// </summary>
        public Style ApplicationBarStyle
        {
            get { return (Style)GetValue(ApplicationBarStyleProperty); }
            set { SetValue(ApplicationBarStyleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the background content of this window instance.
        /// </summary>
        public int HeaderHeight
        {
            get
            {
                var value = GetValue(HeaderHeightProperty);
                if (value != null) return (int)value;
                return 0;
            }
            set { SetValue(HeaderHeightProperty, value); }
        }

        /// <summary>
        /// Get or set commands in Application Bar.
        /// </summary>
        public ObservableCollection<CommandButton> Commands
        {
            get
            {
                return (ObservableCollection<CommandButton>)GetValue(CommandsProperty);
            }

            set
            {
                SetValue(CommandsProperty, value);
            }
        }
    }
}