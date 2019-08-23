// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using My.CoachManager.Presentation.Wpf.Controls.Helpers;

namespace My.CoachManager.Presentation.Wpf.Controls
{
    public class WorkspaceDialog : Frame
    {
        private IInputElement _restoreFocus;

        private bool _saveEnabled;

        public WorkspaceDialog()
        {
            DefaultStyleKey = typeof(WorkspaceDialog);
            Visibility = Visibility.Collapsed;
        }

        static WorkspaceDialog()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WorkspaceDialog),
                new FrameworkPropertyMetadata(typeof(WorkspaceDialog)));
        }

        /// <summary>
        /// Identifies the BackgroundContent dependency property.
        /// </summary>
        public static readonly DependencyProperty OverlayBackgroundProperty = DependencyProperty.Register("OverlayBackground", typeof(SolidColorBrush), typeof(WorkspaceDialog));

        /// <summary>
        /// Gets or sets the background content of this window instance.
        /// </summary>
        public SolidColorBrush OverlayBackground
        {
            get => (SolidColorBrush)GetValue(OverlayBackgroundProperty);
            set => SetValue(OverlayBackgroundProperty, value);
        }

        /// <summary>
        /// Identifies the BackgroundContent dependency property.
        /// </summary>
        public static readonly DependencyProperty OwnerProperty = DependencyProperty.Register("Owner", typeof(UIElement), typeof(WorkspaceDialog));

        /// <summary>
        /// Gets or sets the background content of this window instance.
        /// </summary>
        public UIElement Owner
        {
            get => (UIElement)GetValue(OwnerProperty);
            set => SetValue(OwnerProperty, value);
        }

        public bool Show()
        {
            if (Visibility == Visibility.Visible) return true;
            Visibility = Visibility.Visible;

            if (Owner == null) return false;
            _saveEnabled = Owner.IsEnabled;
            StoreFocus();
            Owner.IsEnabled = false;

            return true;
        }

        public void Hide()
        {
            if (Visibility != Visibility.Visible) return;

            Visibility = Visibility.Collapsed;
            if (Owner != null)
            {
                Owner.IsEnabled = _saveEnabled;
            }
            RestoreFocus();
        }

        /// <summary>
        /// Stores the given element, or the last focused element via FocusManager, for restoring the focus after closing a dialog.
        /// </summary>
        /// <param name="thisElement">The element which will be focused again.</param>
        public void StoreFocus(IInputElement thisElement = null)
        {
            if (Owner != null)
            {
                var window = Owner.FindVisualParent<Window>();
                Dispatcher?.BeginInvoke(new Action(() =>
                {
                    _restoreFocus = thisElement ?? (_restoreFocus ?? FocusManager.GetFocusedElement(window));
                }));
            }
        }

        internal void RestoreFocus()
        {
            if (_restoreFocus != null)
            {
                Dispatcher?.BeginInvoke(new Action(() =>
                {
                    Keyboard.Focus(_restoreFocus);
                    _restoreFocus = null;
                }));
            }
        }
    }
}