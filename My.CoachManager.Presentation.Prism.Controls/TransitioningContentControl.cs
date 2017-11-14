// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using My.CoachManager.Presentation.Prism.Controls.Helpers;

namespace My.CoachManager.Presentation.Prism.Controls
{
    /// <summary>
    /// A ContentControl that animates content as it loads and unloads.
    /// </summary>
    public class TransitioningContentControl : ContentControl
    {
        internal const string PresentationGroup = "PresentationStates";
        internal const string NormalState = "Normal";
        internal const string PreviousContentPresentationSitePartName = "PreviousContentPresentationSite";
        internal const string CurrentContentPresentationSitePartName = "CurrentContentPresentationSite";

        private ContentPresenter _currentContentPresentationSite;
        private ContentPresenter _previousContentPresentationSite;
        private bool _allowIsTransitioningPropertyWrite;
        private Storyboard _currentTransition;

        public event RoutedEventHandler TransitionCompleted;

        public const TransitionType DefaultTransitionState = TransitionType.Default;

        public static readonly DependencyProperty IsTransitioningProperty = DependencyProperty.Register("IsTransitioning", typeof(bool), typeof(TransitioningContentControl), new PropertyMetadata(OnIsTransitioningPropertyChanged));
        public static readonly DependencyProperty TransitionProperty = DependencyProperty.Register("Transition", typeof(TransitionType), typeof(TransitioningContentControl), new FrameworkPropertyMetadata(TransitionType.Default, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.Inherits, OnTransitionPropertyChanged));
        public static readonly DependencyProperty RestartTransitionOnContentChangeProperty = DependencyProperty.Register("RestartTransitionOnContentChange", typeof(bool), typeof(TransitioningContentControl), new PropertyMetadata(false, OnRestartTransitionOnContentChangePropertyChanged));
        public static readonly DependencyProperty CustomVisualStatesProperty = DependencyProperty.Register("CustomVisualStates", typeof(ObservableCollection<VisualState>), typeof(TransitioningContentControl), new PropertyMetadata(null));
        public static readonly DependencyProperty CustomVisualStatesNameProperty = DependencyProperty.Register("CustomVisualStatesName", typeof(string), typeof(TransitioningContentControl), new PropertyMetadata("CustomTransition"));

        public ObservableCollection<VisualState> CustomVisualStates
        {
            get { return (ObservableCollection<VisualState>)GetValue(CustomVisualStatesProperty); }
            set { SetValue(CustomVisualStatesProperty, value); }
        }

        /// <summary>
        /// Gets or sets the name of the custom transition visual state.
        /// </summary>
        public string CustomVisualStatesName
        {
            get { return (string)GetValue(CustomVisualStatesNameProperty); }
            set { SetValue(CustomVisualStatesNameProperty, value); }
        }

        /// <summary>
        /// Gets/sets if the content is transitioning.
        /// </summary>
        public bool IsTransitioning
        {
            get
            {
                var value = GetValue(IsTransitioningProperty);
                return value != null && (bool)value;
            }
            private set
            {
                _allowIsTransitioningPropertyWrite = true;
                SetValue(IsTransitioningProperty, value);
                _allowIsTransitioningPropertyWrite = false;
            }
        }

        public TransitionType Transition
        {
            get
            {
                var value = GetValue(TransitionProperty);
                if (value != null) return (TransitionType)value;
                return default(TransitionType);
            }
            set { SetValue(TransitionProperty, value); }
        }

        public bool RestartTransitionOnContentChange
        {
            get
            {
                var value = GetValue(RestartTransitionOnContentChangeProperty);
                return value != null && (bool)value;
            }
            set { SetValue(RestartTransitionOnContentChangeProperty, value); }
        }

        private static void OnIsTransitioningPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = (TransitioningContentControl)d;

            if (!source._allowIsTransitioningPropertyWrite)
            {
                source.IsTransitioning = (bool)e.OldValue;
                throw new InvalidOperationException();
            }
        }

        private Storyboard CurrentTransition
        {
            get { return _currentTransition; }
            set
            {
                // decouple event
                if (_currentTransition != null)
                {
                    _currentTransition.Completed -= OnTransitionCompleted;
                }

                _currentTransition = value;

                if (_currentTransition != null)
                {
                    _currentTransition.Completed += OnTransitionCompleted;
                }
            }
        }

        private static void OnTransitionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = (TransitioningContentControl)d;
            var oldTransition = (TransitionType)e.OldValue;
            var newTransition = (TransitionType)e.NewValue;

            if (source.IsTransitioning)
            {
                source.AbortTransition();
            }

            // find new transition
            Storyboard newStoryboard = source.GetStoryboard(newTransition);

            // unable to find the transition.
            if (newStoryboard == null)
            {
                // could be during initialization of xaml that presentationgroups was not yet defined
                if (VisualStates.TryGetVisualStateGroup(source, PresentationGroup) == null)
                {
                    // will delay check
                    source.CurrentTransition = null;
                }
                else
                {
                    // revert to old value
                    source.SetValue(TransitionProperty, oldTransition);

                    throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "Temporary removed exception message"));
                }
            }
            else
            {
                source.CurrentTransition = newStoryboard;
            }
        }

        private static void OnRestartTransitionOnContentChangePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((TransitioningContentControl)d).OnRestartTransitionOnContentChangeChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        protected virtual void OnRestartTransitionOnContentChangeChanged(bool oldValue, bool newValue)
        {
        }

        public TransitioningContentControl()
        {
            CustomVisualStates = new ObservableCollection<VisualState>();
            DefaultStyleKey = typeof(TransitioningContentControl);
        }

        public override void OnApplyTemplate()
        {
            if (IsTransitioning)
            {
                AbortTransition();
            }

            if (CustomVisualStates != null && CustomVisualStates.Any())
            {
                var presentationGroup = VisualStates.TryGetVisualStateGroup(this, PresentationGroup);
                if (presentationGroup != null)
                {
                    foreach (var state in CustomVisualStates)
                    {
                        presentationGroup.States.Add(state);
                    }
                }
            }

            base.OnApplyTemplate();

            _previousContentPresentationSite = GetTemplateChild(PreviousContentPresentationSitePartName) as ContentPresenter;
            _currentContentPresentationSite = GetTemplateChild(CurrentContentPresentationSitePartName) as ContentPresenter;

            if (_currentContentPresentationSite != null)
            {
                if (ContentTemplateSelector != null)
                {
                    _currentContentPresentationSite.ContentTemplate = ContentTemplateSelector.SelectTemplate(Content, this);
                }
                else
                {
                    _currentContentPresentationSite.ContentTemplate = ContentTemplate;
                }
                _currentContentPresentationSite.Content = Content;
            }

            // hookup currenttransition
            Storyboard transition = GetStoryboard(Transition);
            CurrentTransition = transition;
            if (transition == null)
            {
                var invalidTransition = Transition;
                // revert to default
                Transition = DefaultTransitionState;

                throw new ArgumentException(string.Format("'{0}' Transition could not be found!", invalidTransition));
            }
            VisualStateManager.GoToState(this, NormalState, false);
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);

            StartTransition(oldContent, newContent);
        }

        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "newContent", Justification = "Should be used in the future.")]
        private void StartTransition(object oldContent, object newContent)
        {
            // both presenters must be available, otherwise a transition is useless.
            if (_currentContentPresentationSite != null && _previousContentPresentationSite != null)
            {
                if (RestartTransitionOnContentChange)
                {
                    CurrentTransition.Completed -= OnTransitionCompleted;
                }

                if (ContentTemplateSelector != null)
                {
                    _previousContentPresentationSite.ContentTemplate = ContentTemplateSelector.SelectTemplate(oldContent, this);
                    _currentContentPresentationSite.ContentTemplate = ContentTemplateSelector.SelectTemplate(newContent, this);
                }
                else
                {
                    _previousContentPresentationSite.ContentTemplate = ContentTemplate;
                    _currentContentPresentationSite.ContentTemplate = ContentTemplate;
                }
                _currentContentPresentationSite.Content = newContent;
                _previousContentPresentationSite.Content = oldContent;

                // and start a new transition
                if (!IsTransitioning || RestartTransitionOnContentChange)
                {
                    if (RestartTransitionOnContentChange)
                    {
                        CurrentTransition.Completed += OnTransitionCompleted;
                    }
                    IsTransitioning = true;
                    VisualStateManager.GoToState(this, NormalState, false);
                    VisualStateManager.GoToState(this, GetTransitionName(Transition), true);
                }
            }
        }

        /// <summary>
        /// Reload the current transition if the content is the same.
        /// </summary>
        public void ReloadTransition()
        {
            // both presenters must be available, otherwise a transition is useless.
            if (_currentContentPresentationSite != null && _previousContentPresentationSite != null)
            {
                if (RestartTransitionOnContentChange)
                {
                    CurrentTransition.Completed -= OnTransitionCompleted;
                }
                if (!IsTransitioning || RestartTransitionOnContentChange)
                {
                    if (RestartTransitionOnContentChange)
                    {
                        CurrentTransition.Completed += OnTransitionCompleted;
                    }
                    IsTransitioning = true;
                    VisualStateManager.GoToState(this, NormalState, false);
                    VisualStateManager.GoToState(this, GetTransitionName(Transition), true);
                }
            }
        }

        private void OnTransitionCompleted(object sender, EventArgs e)
        {
            var clockGroup = sender as ClockGroup;
            AbortTransition();
            if (clockGroup == null || clockGroup.CurrentState == ClockState.Stopped)
            {
                if (TransitionCompleted != null) TransitionCompleted.Invoke(this, new RoutedEventArgs());
            }
        }

        public void AbortTransition()
        {
            // go to normal state and release our hold on the old content.
            VisualStateManager.GoToState(this, NormalState, false);
            IsTransitioning = false;
            if (_previousContentPresentationSite != null)
            {
                _previousContentPresentationSite.ContentTemplate = null;
                _previousContentPresentationSite.Content = null;
            }
        }

        private Storyboard GetStoryboard(TransitionType newTransition)
        {
            VisualStateGroup presentationGroup = VisualStates.TryGetVisualStateGroup(this, PresentationGroup);
            Storyboard newStoryboard = null;
            if (presentationGroup != null)
            {
                var transitionName = GetTransitionName(newTransition);
                newStoryboard = presentationGroup.States
                                                 .OfType<VisualState>()
                                                 .Where(state => state.Name == transitionName)
                                                 .Select(state => state.Storyboard)
                                                 .FirstOrDefault();
            }
            return newStoryboard;
        }

        private string GetTransitionName(TransitionType transition)
        {
            switch (transition)
            {
                default:
                    return "DefaultTransition";

                case TransitionType.Normal:
                    return "Normal";

                case TransitionType.Up:
                    return "UpTransition";

                case TransitionType.Down:
                    return "DownTransition";

                case TransitionType.Right:
                    return "RightTransition";

                case TransitionType.RightReplace:
                    return "RightReplaceTransition";

                case TransitionType.Left:
                    return "LeftTransition";

                case TransitionType.LeftReplace:
                    return "LeftReplaceTransition";

                case TransitionType.Custom:
                    return CustomVisualStatesName;
            }
        }
    }

    /// <summary>
    /// enumeration for the different transition types
    /// </summary>
    public enum TransitionType
    {
        /// <summary>
        /// Use the VisualState DefaultTransition
        /// </summary>
        Default,

        /// <summary>
        /// Use the VisualState Normal
        /// </summary>
        Normal,

        /// <summary>
        /// Use the VisualState UpTransition
        /// </summary>
        Up,

        /// <summary>
        /// Use the VisualState DownTransition
        /// </summary>
        Down,

        /// <summary>
        /// Use the VisualState RightTransition
        /// </summary>
        Right,

        /// <summary>
        /// Use the VisualState RightReplaceTransition
        /// </summary>
        RightReplace,

        /// <summary>
        /// Use the VisualState LeftTransition
        /// </summary>
        Left,

        /// <summary>
        /// Use the VisualState LeftReplaceTransition
        /// </summary>
        LeftReplace,

        /// <summary>
        /// Use a custom VisualState, the name must be set using CustomVisualStatesName property
        /// </summary>
        Custom
    }
}