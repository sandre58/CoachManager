﻿using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace My.CoachManager.Presentation.Wpf.Controls
{
    [TemplatePart(Name = DecorName, Type = typeof(Ellipse))]
    [TemplatePart(Name = HeaderHostName, Type = typeof(ContentPresenter))]
    [TemplatePart(Name = ContentHostName, Type = typeof(ContentPresenter))]
    public abstract class CommandButtonBase : Button
    {
        private const string DecorName = "PART_Decor";
        private const string HeaderHostName = "PART_HeaderHost";
        private const string ContentHostName = "PART_ContentHost";

        private Ellipse _decor;
        private ContentPresenter _headerHost;
        private ContentPresenter _contentHost;

        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = "We need to use static constructor for custom actions during dependency properties initialization")]
        static CommandButtonBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CommandButtonBase), new FrameworkPropertyMetadata(typeof(CommandButtonBase)));
        }

        public static readonly DependencyProperty ButtonDiameterProperty =
            DependencyProperty.Register("ButtonDiameter", typeof(double), typeof(CommandButtonBase),
                new FrameworkPropertyMetadata(20.0, FrameworkPropertyMetadataOptions.None));


        [Bindable(true)]
        [Category("Content")]
        [Description("The icon size of each control.")]
        [Localizability(LocalizationCategory.Label)]
        public double ButtonDiameter
        {
            get { return (double)GetValue(ButtonDiameterProperty); }
            set { SetValue(ButtonDiameterProperty, value); }
        }

        public static readonly DependencyProperty HeaderProperty =
            HeaderedContentControl.HeaderProperty.AddOwner(typeof(CommandButtonBase),
                                                           new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, OnHeaderChanged));

        
        [Bindable(true)]
        [Category("Content")]
        [Description("The data used for the header of each control.")]
        [Localizability(LocalizationCategory.Label)]
        public object Header
        {
            get { return GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        private static void OnHeaderChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var instance = (CommandButtonBase)obj;
            instance.OnHeaderChanged(e.OldValue, e.NewValue);
            instance.HasHeader = e.NewValue != null;
        }

        
        // ReSharper disable VirtualMemberNeverOverriden.Global
        protected virtual void OnHeaderChanged(object oldHeader, object newHeader)
        // ReSharper restore VirtualMemberNeverOverriden.Global
        {
            RemoveLogicalChild(oldHeader);
            AddLogicalChild(newHeader);
        }

        private static readonly DependencyPropertyKey HasHeaderPropertyKey =
            DependencyProperty.RegisterReadOnly("HasHeader", typeof(bool), typeof(CommandButtonBase),
                                                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.None,
                                                                              OnHasHeaderChanged));

        
        public static readonly DependencyProperty HasHeaderProperty = HasHeaderPropertyKey.DependencyProperty;

        
        [Bindable(false)]
        [Browsable(false)]
        public bool HasHeader
        {
            get { return (bool)GetValue(HasHeaderProperty); }
            private set { SetValue(HasHeaderPropertyKey,value); }
        }

        private static void OnHasHeaderChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var instance = (CommandButtonBase)obj;
            instance.OnHasHeaderChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        
        // ReSharper disable VirtualMemberNeverOverriden.Global
        protected virtual void OnHasHeaderChanged(bool oldHasHeader, bool newHasHeader)
        // ReSharper restore VirtualMemberNeverOverriden.Global
        {
        }

        
        public static readonly DependencyProperty HeaderStringFormatProperty =
            HeaderedContentControl.HeaderStringFormatProperty.AddOwner(typeof(CommandButtonBase),
                                                                       new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None,
                                                                                                     OnHeaderStringFormatChanged));

        
        [Bindable(true)]
        [Category("Content")]
        [Description("A composite string that specifies how to format the Header property if it is displayed as a string.")]
        public string HeaderStringFormat
        {
            get { return (string)GetValue(HeaderStringFormatProperty); }
            set { SetValue(HeaderStringFormatProperty, value); }
        }

        private static void OnHeaderStringFormatChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var instance = (CommandButtonBase)obj;
            instance.OnHeaderStringFormatChanged((string)e.OldValue, (string)e.NewValue);
        }

        
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string", Justification = "String is not a type name. String is a part of phrase StringFormat.")]
        // ReSharper disable VirtualMemberNeverOverriden.Global
        protected virtual void OnHeaderStringFormatChanged(string oldHeaderStringFormat, string newHeaderStringFormat)
        // ReSharper restore VirtualMemberNeverOverriden.Global
        {
        }

        
        public static readonly DependencyProperty HeaderTemplateProperty =
            HeaderedContentControl.HeaderTemplateProperty.AddOwner(typeof(CommandButtonBase),
                                                                   new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None,
                                                                                                 OnHeaderTemplateChanged));

        
        [Bindable(true)]
        [Category("Content")]
        [Description("The template used to display the content of the control's header.")]
        public DataTemplate HeaderTemplate
        {
            get { return (DataTemplate)GetValue(HeaderTemplateProperty); }
            set { SetValue(HeaderTemplateProperty, value); }
        }

        private static void OnHeaderTemplateChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var instance = (CommandButtonBase)obj;
            instance.OnHeaderTemplateChanged((DataTemplate)e.OldValue, (DataTemplate)e.NewValue);
        }

        
        // ReSharper disable VirtualMemberNeverOverriden.Global
        protected virtual void OnHeaderTemplateChanged(DataTemplate oldHeaderTemplate, DataTemplate newHeaderTemplate)
        // ReSharper restore VirtualMemberNeverOverriden.Global
        {
            if (newHeaderTemplate != null && HeaderTemplateSelector != null)
            {
                Trace.TraceError("HeaderTemplate and HeaderTemplateSelector defined");
            }
        }

        
        public static readonly DependencyProperty HeaderTemplateSelectorProperty =
            HeaderedContentControl.HeaderTemplateSelectorProperty.AddOwner(typeof(CommandButtonBase),
                                                                           new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None,
                                                                                                         OnHeaderTemplateSelectorChanged));

        
        [Bindable(true)]
        [Category("Content")]
        [Description("A data template selector that provides custom logic for choosing the template used to display the header.")]
        public DataTemplateSelector HeaderTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(HeaderTemplateSelectorProperty); }
            set { SetValue(HeaderTemplateSelectorProperty, value); }
        }

        private static void OnHeaderTemplateSelectorChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var instance = (CommandButtonBase)obj;
            instance.OnHeaderTemplateSelectorChanged((DataTemplateSelector)e.OldValue, (DataTemplateSelector)e.NewValue);
        }

        
        // ReSharper disable VirtualMemberNeverOverriden.Global
        protected virtual void OnHeaderTemplateSelectorChanged(DataTemplateSelector oldHeaderTemplateSelector, DataTemplateSelector newHeaderTemplateSelector)
        // ReSharper restore VirtualMemberNeverOverriden.Global
        {
            if (HeaderTemplate != null && newHeaderTemplateSelector != null)
            {
                Trace.TraceError("HeaderTemplate and HeaderTemplateSelector defined");
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (Template != null)
            {
                _decor = Template.FindName(DecorName, this) as Ellipse;
                if (_decor == null)
                {
                    Trace.TraceError(DecorName + " not found.");
                }

                // NOTE: Lack of contracts: FindName must be marked as pure method
                Contract.Assume(Template != null);
                _headerHost = Template.FindName(HeaderHostName, this) as ContentPresenter;
                if (_headerHost == null)
                {
                    Trace.TraceError(HeaderHostName + " not found.");
                }

                // NOTE: Lack of contracts: FindName must be marked as pure method
                Contract.Assume(Template != null);
                _contentHost = Template.FindName(ContentHostName, this) as ContentPresenter;
                if (_contentHost == null)
                {
                    Trace.TraceError(ContentHostName + " not found.");
                }
            }
        }

        //protected override Size MeasureOverride(Size constraint)
        //{
        //    if (_decor != null && _headerHost != null && _contentHost != null)
        //    {
        //        var infinitySize = new Size(double.PositiveInfinity, double.PositiveInfinity);
        //        _contentHost.Measure(infinitySize);
        //        _headerHost.Measure(infinitySize);

        //        // NOTE: Lack of contracts: DesiredSize.Width and DesiredSize.Height must ensure non-negative value
        //        Contract.Assume(_contentHost.DesiredSize.Width >= 0d);
        //        Contract.Assume(_contentHost.DesiredSize.Height >= 0d);
        //        Contract.Assume(_headerHost.DesiredSize.Width >= 0d);
        //        Contract.Assume(_headerHost.DesiredSize.Height >= 0d);

        //        var contentSize = Math.Max(_contentHost.DesiredSize.Width, _contentHost.DesiredSize.Height);
        //        var contentBoxSize = Math.Ceiling(contentSize * Math.Sqrt(2d));

        //        // NOTE: Parity must be the same
        //        if (((int)contentBoxSize % 2 == 0) ^ ((int)Math.Ceiling(contentSize) % 2 == 0))
        //        {
        //            contentBoxSize++;
        //        }

        //        var constraintWidth = double.IsNaN(constraint.Width) || double.IsInfinity(constraint.Width) ? double.MaxValue : constraint.Width;
        //        var constraintHeight = double.IsNaN(constraint.Height) || double.IsInfinity(constraint.Height) ? 0d : constraint.Height;

        //        var width = Math.Min(Math.Max(contentBoxSize, _headerHost.DesiredSize.Width), constraintWidth);
        //        var height = Math.Max(contentBoxSize + _headerHost.DesiredSize.Height, constraintHeight);

        //        // NOTE: Lack of contracts: Math.Max and Math.Min doesn't contains ensures
        //        Contract.Assume(width >= 0d);
        //        Contract.Assume(height >= 0d);

        //        var boxSize = Math.Min(width, Math.Max(height - _headerHost.DesiredSize.Height, 0d));

        //        // Parity must be the same
        //        if (((int)boxSize % 2 == 0) ^ ((int)Math.Ceiling(contentSize) % 2 == 0))
        //        {
        //            boxSize--;
        //        }

        //        // NOTE: Lack of contracts: Math.Max and Math.Min doesn't contains ensures
        //        Contract.Assume(boxSize >= 0d);

        //        _decor.Width = boxSize;
        //        _decor.Height = boxSize;

        //        var finalSize = new Size(width, height);
        //        _contentHost.Measure(finalSize);
        //        _headerHost.Measure(finalSize);

        //        return finalSize;
        //    }
        //    return base.MeasureOverride(constraint);
        //}

        //protected override Size ArrangeOverride(Size arrangeBounds)
        //{
        //    if (_decor != null && _headerHost != null && _contentHost != null)
        //    {
        //        var infinitySize = new Size(double.PositiveInfinity, double.PositiveInfinity);
        //        _contentHost.Measure(infinitySize);
        //        _headerHost.Measure(infinitySize);

        //        var contentSize = Math.Max(_contentHost.DesiredSize.Width, _contentHost.DesiredSize.Height);
        //        var boxSize = Math.Min(arrangeBounds.Width, Math.Max(arrangeBounds.Height - _headerHost.DesiredSize.Height, 0d));

        //        // NOTE: Parity must be the same
        //        if (((int)boxSize % 2 == 0) ^ ((int)Math.Ceiling(contentSize) % 2 == 0))
        //        {
        //            boxSize--;
        //        }

        //        // NOTE: Lack of contracts: Math.Max and Math.Min doesn't contains ensures
        //        Contract.Assume(boxSize >= 0d);

        //        _decor.Width = boxSize;
        //        _decor.Height = boxSize;

        //        _contentHost.Measure(arrangeBounds);
        //        _headerHost.Measure(arrangeBounds);
        //    }
        //    return base.ArrangeOverride(arrangeBounds);
        //}
    }
}
