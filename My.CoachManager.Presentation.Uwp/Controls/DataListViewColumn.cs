﻿using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Microsoft.Toolkit.Uwp.UI.Controls;

namespace My.CoachManager.Presentation.Uwp.Controls
{
    public abstract class DataListViewColumn : DependencyObject
    {
        public static readonly DependencyProperty CanSortProperty =
            DependencyProperty.Register("CanSort", typeof(bool), typeof(DataListViewColumn), new PropertyMetadata(true));

        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(DataListViewColumn), new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty IsAscendingProperty =
            DependencyProperty.Register("IsAscending", typeof(bool), typeof(DataListViewColumn), new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty IsAscendingDefaultProperty =
            DependencyProperty.Register("IsAscendingDefault", typeof(bool), typeof(DataListViewColumn), new PropertyMetadata(true));

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(object), typeof(DataListViewColumn), new PropertyMetadata(default(object)));

        public static readonly DependencyProperty WidthProperty =
            DependencyProperty.Register("Width", typeof(GridLength), typeof(DataListViewColumn), new PropertyMetadata(default(GridLength)));

        /// <summary>Gets or sets a value indicating whether the column can be sorted. </summary>
        public bool CanSort
        {
            get => (bool)GetValue(CanSortProperty);
            set => SetValue(CanSortProperty, value);
        }

        /// <summary>Gets a value indicating whether the the column is selected and used for sorting. 
        /// This property should not be set directly, use the SelectColumn method on <see cref="DataGrid"/>. </summary>
        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            internal set => SetValue(IsSelectedProperty, value);
        }

        /// <summary>Gets a value indicating whether the column is sorted ascending (otherwise descending). 
        /// This property should not be set directly, use the SelectColumn method on <see cref="DataGrid"/>. </summary>
        public bool IsAscending
        {
            get => (bool)GetValue(IsAscendingProperty);
            internal set => SetValue(IsAscendingProperty, value);
        }

        /// <summary>Gets or sets a value indicating whether ascending sorting is default 
        /// (first click on the column will sort it ascending, otherwise descending). </summary>
        public bool IsAscendingDefault
        {
            get => (bool)GetValue(IsAscendingDefaultProperty);
            set => SetValue(IsAscendingDefaultProperty, value);
        }

        /// <summary>Gets or sets the header. </summary>
        public object Header
        {
            get => GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        /// <summary>Gets or sets the width of the column. </summary>
        public GridLength Width
        {
            get => (GridLength)GetValue(WidthProperty);
            set => SetValue(WidthProperty, value);
        }

        /// <summary>Gets the property path which is used for sorting. </summary>
        public abstract PropertyPath OrderPropertyPath { get; }

        /// <summary>Creates a new column definition </summary>
        /// <returns>The column definition. </returns>
        internal ColumnDefinition CreateGridColumnDefinition()
        {
            return new ColumnDefinition { Width = Width };
        }

        protected void CreateBinding(
            DependencyProperty sourceDependencyProperty,
            string sourceDependencyPropertyName,
            FrameworkElement targetElement,
            DependencyProperty targetElementDependencyProperty)
        {
            if (ReadLocalValue(sourceDependencyProperty) is BindingExpression binding)
                targetElement.SetBinding(targetElementDependencyProperty, binding.ParentBinding);
            else
            {
                targetElement.SetBinding(targetElementDependencyProperty, new Binding
                {
                    Path = new PropertyPath(sourceDependencyPropertyName),
                    Source = this
                });
            }
        }
    }
}
