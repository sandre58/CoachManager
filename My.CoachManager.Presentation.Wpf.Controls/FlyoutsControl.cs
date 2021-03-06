﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using My.CoachManager.Presentation.Wpf.Controls.Helpers;
using My.CoachManager.Presentation.Wpf.Controls.Windows;

namespace My.CoachManager.Presentation.Wpf.Controls
{
    /// <summary>
    /// A FlyoutsControl is for displaying flyouts in a MetroWindow.
    /// <see cref="ExtendedWindow"/>
    /// </summary>
    [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(Flyout))]
    public class FlyoutsControl : ItemsControl
    {
        public static readonly DependencyProperty OverrideExternalCloseButtonProperty = DependencyProperty.Register("OverrideExternalCloseButton", typeof(MouseButton?), typeof(FlyoutsControl), new PropertyMetadata(null));
        public static readonly DependencyProperty OverrideIsPinnedProperty = DependencyProperty.Register("OverrideIsPinned", typeof(bool), typeof(FlyoutsControl), new PropertyMetadata(false));

        /// <summary>
        /// Gets/sets whether <see cref="Flyout.ExternalCloseButton"/> is ignored and all flyouts behave as if it was set to the value of this property.
        /// </summary>
        public MouseButton? OverrideExternalCloseButton
        {
            get { return (MouseButton?)GetValue(OverrideExternalCloseButtonProperty); }
            set { SetValue(OverrideExternalCloseButtonProperty, value); }
        }

        /// <summary>
        /// Gets/sets whether <see cref="Flyout.IsPinned"/> is ignored and all flyouts behave as if it was set false.
        /// </summary>
        public bool OverrideIsPinned
        {
            get { return (bool)GetValue(OverrideIsPinnedProperty); }
            set { SetValue(OverrideIsPinnedProperty, value); }
        }

        static FlyoutsControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(FlyoutsControl), new FrameworkPropertyMetadata(typeof(FlyoutsControl)));
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new Flyout();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is Flyout;
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);
            AttachHandlers((Flyout)element);
        }

        protected override void ClearContainerForItemOverride(DependencyObject element, object item)
        {
            ((Flyout)element).CleanUp(this);
            base.ClearContainerForItemOverride(element, item);
        }

        private void AttachHandlers(Flyout flyout)
        {
            var isOpenNotifier = new PropertyChangeNotifier(flyout, Flyout.IsOpenProperty);
            isOpenNotifier.ValueChanged += FlyoutStatusChanged;
            flyout.IsOpenPropertyChangeNotifier = isOpenNotifier;
        }

        private void FlyoutStatusChanged(object sender, EventArgs e)
        {
            var flyout = GetFlyout(sender); //Get the flyout that raised the handler.

            HandleFlyoutStatusChange(flyout, this.TryFindParent<ExtendedWindow>());
        }

        internal void HandleFlyoutStatusChange(Flyout flyout, ExtendedWindow parentWindow)
        {
            if (flyout == null || parentWindow == null)
            {
                return;
            }

            ReorderZIndices(flyout);

            var visibleFlyouts = GetFlyouts(Items).Where(i => i.IsOpen).OrderBy(Panel.GetZIndex);
            parentWindow.HandleFlyoutStatusChange(flyout, visibleFlyouts);
        }

        private Flyout GetFlyout(object item)
        {
            var flyout = item as Flyout;
            if (flyout != null)
            {
                return flyout;
            }

            return (Flyout)ItemContainerGenerator.ContainerFromItem(item);
        }

        internal IEnumerable<Flyout> GetFlyouts()
        {
            return GetFlyouts(Items);
        }

        private IEnumerable<Flyout> GetFlyouts(IEnumerable items)
        {
            return from object item in items select GetFlyout(item);
        }

        private void ReorderZIndices(Flyout lastChanged)
        {
            var openFlyouts = GetFlyouts(Items).Where(i => i.IsOpen && !Equals(i, lastChanged)).OrderBy(Panel.GetZIndex);
            var index = 0;
            foreach (var openFlyout in openFlyouts)
            {
                Panel.SetZIndex(openFlyout, index);
                index++;
            }

            if (lastChanged.IsOpen)
            {
                Panel.SetZIndex(lastChanged, index);
            }
        }
    }
}