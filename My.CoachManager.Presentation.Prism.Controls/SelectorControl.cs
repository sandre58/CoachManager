using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.Presentation.Prism.Controls.Helpers;

namespace My.CoachManager.Presentation.Prism.Controls
{
    [TemplatePart(Name = PartAll, Type = typeof(SelectorItem))]
    public class SelectorControl : ItemsControl, IWeakEventListener //should probably make this control an ICommandSource
    {
        #region Members

        private const string PartAll = "PART_All";

        private bool _surpressItemSelectionChanged;
        private bool _ignoreSelectedItemChanged;
        private bool _ignoreSelectedValueChanged;
        private int _ignoreSelectedItemsCollectionChanged;
        private int _ignoreSelectedMemberPathValuesChanged;
        private IList _selectedItems;
        private SelectorItem _allItem;
        private readonly IList _removedItems = new ObservableCollection<object>();

        private readonly ValueChangeHelper _selectedMemberPathValuesHelper;
        private readonly ValueChangeHelper _valueMemberPathValuesHelper;

        #endregion Members

        #region Constructors

        public SelectorControl()
        {
            SelectedItems = new ObservableCollection<object>();
            AddHandler(SelectedEvent, new RoutedEventHandler((s, args) => OnItemSelectionChangedCore(args, false)));
            AddHandler(UnSelectedEvent, new RoutedEventHandler((s, args) => OnItemSelectionChangedCore(args, true)));
            _selectedMemberPathValuesHelper = new ValueChangeHelper(OnSelectedMemberPathValuesChanged);
            _valueMemberPathValuesHelper = new ValueChangeHelper(OnValueMemberPathValuesChanged);
        }

        #endregion Constructors

        #region Properties

        public static readonly DependencyProperty TextAllProperty = DependencyProperty.Register("TextAll", typeof(string), typeof(SelectorControl), new PropertyMetadata(GlobalResources.AllMales));

        [TypeConverter(typeof(CommandConverter))]
        public string TextAll
        {
            get
            {
                return (string)GetValue(TextAllProperty);
            }
            set
            {
                SetValue(TextAllProperty, value);
            }
        }

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(SelectorControl), new PropertyMetadata((ICommand)null));

        [TypeConverter(typeof(CommandConverter))]
        public ICommand Command
        {
            get
            {
                return (ICommand)GetValue(CommandProperty);
            }
            set
            {
                SetValue(CommandProperty, value);
            }
        }

        #region Delimiter

        public static readonly DependencyProperty DelimiterProperty = DependencyProperty.Register("Delimiter", typeof(string), typeof(SelectorControl), new UIPropertyMetadata(",", OnDelimiterChanged));

        public string Delimiter
        {
            get
            {
                return (string)GetValue(DelimiterProperty);
            }
            set
            {
                SetValue(DelimiterProperty, value);
            }
        }

        private static void OnDelimiterChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((SelectorControl)o).OnSelectedItemChanged((string)e.OldValue, (string)e.NewValue);
        }

        protected virtual void OnSelectedItemChanged(string oldValue, string newValue)
        {
            if (!IsInitialized)
                return;

            UpdateSelectedValue();
            UpdateSelectedValues();
        }

        #endregion Delimiter

        #region SelectedItem property

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(object), typeof(SelectorControl), new UIPropertyMetadata(null, OnSelectedItemChanged));

        public object SelectedItem
        {
            get
            {
                return GetValue(SelectedItemProperty);
            }
            set
            {
                SetValue(SelectedItemProperty, value);
            }
        }

        private static void OnSelectedItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            ((SelectorControl)sender).OnSelectedItemChanged(args.OldValue, args.NewValue);
        }

        protected virtual void OnSelectedItemChanged(object oldValue, object newValue)
        {
            if (!IsInitialized || _ignoreSelectedItemChanged)
                return;

            _ignoreSelectedItemsCollectionChanged++;
            SelectedItems.Clear();
            if (newValue != null)
            {
                SelectedItems.Add(newValue);
            }
            UpdateFromSelectedItems();
            _ignoreSelectedItemsCollectionChanged--;
        }

        #endregion SelectedItem property

        #region SelectedItems Property

        public IList SelectedItems
        {
            get
            {
                return _selectedItems;
            }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                INotifyCollectionChanged oldCollection = _selectedItems as INotifyCollectionChanged;
                INotifyCollectionChanged newCollection = value as INotifyCollectionChanged;

                if (oldCollection != null)
                {
                    CollectionChangedEventManager.RemoveListener(oldCollection, this);
                }

                if (newCollection != null)
                {
                    CollectionChangedEventManager.AddListener(newCollection, this);
                }

                _selectedItems = value;
            }
        }

        #endregion SelectedItems Property

        #region SelectedItemsOverride property

        public static readonly DependencyProperty SelectedItemsOverrideProperty = DependencyProperty.Register("SelectedItemsOverride", typeof(IList), typeof(SelectorControl), new UIPropertyMetadata(null, SelectedItemsOverrideChanged));

        public IList SelectedItemsOverride
        {
            get
            {
                return (IList)GetValue(SelectedItemsOverrideProperty);
            }
            set
            {
                SetValue(SelectedItemsOverrideProperty, value);
            }
        }

        private static void SelectedItemsOverrideChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            ((SelectorControl)sender).OnSelectedItemsOverrideChanged((IList)args.OldValue, (IList)args.NewValue);
        }

        protected virtual void OnSelectedItemsOverrideChanged(IList oldValue, IList newValue)
        {
            if (!IsInitialized)
                return;

            SelectedItems = (newValue != null) ? newValue : new ObservableCollection<object>();
            UpdateFromSelectedItems();
        }

        #endregion SelectedItemsOverride property

        #region SelectedValues property

        public static readonly DependencyProperty SelectedValuesProperty = DependencyProperty.Register("SelectedValues", typeof(object), typeof(SelectorControl), new UIPropertyMetadata(null, OnSelectedValuesChanged));

        public object SelectedValues
        {
            get
            {
                return GetValue(SelectedValuesProperty);
            }
            set
            {
                SetValue(SelectedValuesProperty, value);
            }
        }

        private static void OnSelectedValuesChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            ((SelectorControl)sender).OnSelectedValuesChanged(args.OldValue, args.NewValue);
        }

        protected virtual void OnSelectedValuesChanged(object oldValue, object newValue)
        {
            if (_ignoreSelectedValuesChanged)
                return;

            var values = newValue as IList;
            if (values != null)
                SelectedValue = string.Join(Delimiter, values.Cast<object>().ToList());
        }

        #endregion SelectedValues property

        #region SelectedMemberPath Property

        public static readonly DependencyProperty SelectedMemberPathProperty = DependencyProperty.Register("SelectedMemberPath", typeof(string), typeof(SelectorControl), new UIPropertyMetadata(null, OnSelectedMemberPathChanged));

        public string SelectedMemberPath
        {
            get
            {
                return (string)GetValue(SelectedMemberPathProperty);
            }
            set
            {
                SetValue(SelectedMemberPathProperty, value);
            }
        }

        private static void OnSelectedMemberPathChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            SelectorControl sel = ((SelectorControl)o);
            sel.OnSelectedMemberPathChanged((string)e.OldValue, (string)e.NewValue);
        }

        protected virtual void OnSelectedMemberPathChanged(string oldValue, string newValue)
        {
            if (!IsInitialized)
                return;

            UpdateSelectedMemberPathValuesBindings();
        }

        #endregion SelectedMemberPath Property

        #region SelectedValue

        public static readonly DependencyProperty SelectedValueProperty = DependencyProperty.Register("SelectedValue", typeof(string), typeof(SelectorControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedValueChanged));

        public string SelectedValue
        {
            get
            {
                return (string)GetValue(SelectedValueProperty);
            }
            set
            {
                SetValue(SelectedValueProperty, value);
            }
        }

        private static void OnSelectedValueChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            SelectorControl selector = o as SelectorControl;
            if (selector != null)
                selector.OnSelectedValueChanged((string)e.OldValue, (string)e.NewValue);
        }

        protected virtual void OnSelectedValueChanged(string oldValue, string newValue)
        {
            if (!IsInitialized || _ignoreSelectedValueChanged)
                return;

            UpdateFromSelectedValue();
        }

        #endregion SelectedValue

        #region ValueMemberPath

        public static readonly DependencyProperty ValueMemberPathProperty = DependencyProperty.Register("ValueMemberPath", typeof(string), typeof(SelectorControl), new UIPropertyMetadata(OnValueMemberPathChanged));

        public string ValueMemberPath
        {
            get
            {
                return (string)GetValue(ValueMemberPathProperty);
            }
            set
            {
                SetValue(ValueMemberPathProperty, value);
            }
        }

        private static void OnValueMemberPathChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            SelectorControl sel = ((SelectorControl)o);
            sel.OnValueMemberPathChanged((string)e.OldValue, (string)e.NewValue);
        }

        protected virtual void OnValueMemberPathChanged(string oldValue, string newValue)
        {
            if (!IsInitialized)
                return;

            UpdateValueMemberPathValuesBindings();
        }

        #endregion ValueMemberPath

        #region ItemsCollection Property

        protected IEnumerable ItemsCollection
        {
            get
            {
                return ItemsSource ?? (Items);
            }
        }

        #endregion ItemsCollection Property

        #endregion Properties

        #region Base Class Overrides

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _allItem = GetTemplateChild(PartAll) as SelectorItem;
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is SelectorItem;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new SelectorItem();
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);

            _surpressItemSelectionChanged = true;
            var selectorItem = element as FrameworkElement;

            if (selectorItem != null)
                selectorItem.SetValue(SelectorItem.IsSelectedProperty, SelectedItems.Contains(item));

            _surpressItemSelectionChanged = false;
        }

        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            base.OnItemsSourceChanged(oldValue, newValue);

            var oldCollection = oldValue as INotifyCollectionChanged;
            var newCollection = newValue as INotifyCollectionChanged;

            if (oldCollection != null)
            {
                CollectionChangedEventManager.RemoveListener(oldCollection, this);
            }

            if (newCollection != null)
            {
                CollectionChangedEventManager.AddListener(newCollection, this);
            }

            if (!IsInitialized)
                return;

            if (!VirtualizingPanel.GetIsVirtualizing(this)
              || (VirtualizingPanel.GetIsVirtualizing(this) && (newValue != null)))
            {
                RemoveUnavailableSelectedItems();
            }

            UpdateSelectedMemberPathValuesBindings();
            UpdateValueMemberPathValuesBindings();
        }

        // When a DataTemplate includes a CheckComboBox, some bindings are
        // not working, like SelectedValue.
        // We use a priority system to select the good items after initialization.
        public override void EndInit()
        {
            base.EndInit();

            if (SelectedItemsOverride != null)
            {
                OnSelectedItemsOverrideChanged(null, SelectedItemsOverride);
            }
            else if (SelectedMemberPath != null)
            {
                OnSelectedMemberPathChanged(null, SelectedMemberPath);
            }
            else if (SelectedValue != null)
            {
                OnSelectedValueChanged(null, SelectedValue);
            }
            else if (SelectedItem != null)
            {
                OnSelectedItemChanged(null, SelectedItem);
            }

            if (ValueMemberPath != null)
            {
                OnValueMemberPathChanged(null, ValueMemberPath);
            }
        }

        #endregion Base Class Overrides

        #region Events

        public static readonly RoutedEvent SelectedEvent = EventManager.RegisterRoutedEvent("SelectedEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SelectorControl));
        public static readonly RoutedEvent UnSelectedEvent = EventManager.RegisterRoutedEvent("UnSelectedEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SelectorControl));

        public static readonly RoutedEvent ItemSelectionChangedEvent = EventManager.RegisterRoutedEvent("ItemSelectionChanged", RoutingStrategy.Bubble, typeof(ItemSelectionChangedEventHandler), typeof(SelectorControl));

        public event ItemSelectionChangedEventHandler ItemSelectionChanged
        {
            add
            {
                AddHandler(ItemSelectionChangedEvent, value);
            }
            remove
            {
                RemoveHandler(ItemSelectionChangedEvent, value);
            }
        }

        #endregion Events

        #region Methods

        protected object GetPathValue(object item, string propertyPath)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            if (String.IsNullOrEmpty(propertyPath)
              || propertyPath == ".")
                return item;

            PropertyInfo prop = item.GetType().GetProperty(propertyPath);
            return (prop != null)
              ? prop.GetValue(item, null)
              : null;
        }

        protected object GetItemValue(object item)
        {
            return (item != null)
              ? GetPathValue(item, ValueMemberPath)
              : null;
        }

        protected object ResolveItemByValue(string value)
        {
            if (!String.IsNullOrEmpty(ValueMemberPath))
            {
                foreach (object item in ItemsCollection)
                {
                    var property = item.GetType().GetProperty(ValueMemberPath);
                    if (property != null)
                    {
                        var propertyValue = property.GetValue(item, null);
                        if (value.Equals(propertyValue.ToString(), StringComparison.InvariantCultureIgnoreCase))
                            return item;
                    }
                }
            }

            return value;
        }

        internal void UpdateFromList(List<string> selectedValues, Func<object, object> getItemfunction)
        {
            _ignoreSelectedItemsCollectionChanged++;
            // Just update the SelectedItems collection content
            // and let the synchronization be made from UpdateFromSelectedItems();
            SelectedItems.Clear();

            if ((selectedValues != null) && (selectedValues.Count > 0))
            {
                ValueEqualityComparer comparer = new ValueEqualityComparer();

                foreach (object item in ItemsCollection)
                {
                    object itemValue = getItemfunction(item);

                    bool isSelected = (itemValue != null)
                      && selectedValues.Contains(itemValue.ToString(), comparer);

                    if (isSelected)
                    {
                        SelectedItems.Add(item);
                    }
                }
            }
            _ignoreSelectedItemsCollectionChanged--;

            UpdateFromSelectedItems();
        }

        private bool? GetSelectedMemberPathValue(object item)
        {
            PropertyInfo prop = GetSelectedMemberPathProperty(item);

            return (prop != null)
              ? (bool)prop.GetValue(item, null)
              : (bool?)null;
        }

        private void SetSelectedMemberPathValue(object item, bool value)
        {
            PropertyInfo prop = GetSelectedMemberPathProperty(item);

            if (prop != null)
            {
                prop.SetValue(item, value, null);
            }
        }

        private PropertyInfo GetSelectedMemberPathProperty(object item)
        {
            PropertyInfo propertyInfo = null;
            if (!String.IsNullOrEmpty(SelectedMemberPath) && (item != null))
            {
                var property = item.GetType().GetProperty(SelectedMemberPath);
                if (property != null && property.PropertyType == typeof(bool))
                {
                    propertyInfo = property;
                }
            }

            return propertyInfo;
        }

        /// <summary>
        /// When SelectedItems collection implements INotifyPropertyChanged, this is the callback.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnSelectedItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (_ignoreSelectedItemsCollectionChanged > 0)
                return;

            // Keep it simple for now. Just update all
            UpdateFromSelectedItems();
        }
        
        private bool _ignoreSelectedValuesChanged;

        private void OnItemSelectionChangedCore(RoutedEventArgs args, bool unselected)
        {
            object item = ItemContainerGenerator.ItemFromContainer((DependencyObject)args.OriginalSource);

            // When the item is it's own container, "UnsetValue" will be returned.
            if (item == DependencyProperty.UnsetValue)
            {
                item = args.OriginalSource;
            }

            if (item.Equals(_allItem))
            {
                foreach (var i in ItemContainerGenerator.Items)
                {
                    if (ItemContainerGenerator.ContainerFromItem(i) is SelectorItem container)
                        container.IsSelected = !unselected;
                }
            }
            else
            {
                if (unselected)
                {
                    while (SelectedItems.Contains(item))
                    {
                        SelectedItems.Remove(item);
                    }
                }
                else
                {
                    if (!SelectedItems.Contains(item))
                    {
                        SelectedItems.Add(item);
                    }
                }
            }

            OnItemSelectionChanged(new ItemSelectionChangedEventArgs(ItemSelectionChangedEvent, this, item, !unselected));
        }

        /// <summary>
        /// When the ItemsSource implements INotifyPropertyChanged, this is the change callback.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            RemoveUnavailableSelectedItems();
            AddAvailableRemovedItems();
            UpdateSelectedMemberPathValuesBindings();
            UpdateValueMemberPathValuesBindings();
        }

        /// <summary>
        /// This is called when any value of any item referenced by SelectedMemberPath
        /// is modified. This may affect the SelectedItems collection.
        /// </summary>
        private void OnSelectedMemberPathValuesChanged()
        {
            if (_ignoreSelectedMemberPathValuesChanged > 0)
                return;

            UpdateFromSelectedMemberPathValues();
        }

        /// <summary>
        /// This is called when any value of any item referenced by ValueMemberPath
        /// is modified. This will affect the SelectedValue property
        /// </summary>
        private void OnValueMemberPathValuesChanged()
        {
            UpdateSelectedValue();
            UpdateSelectedValues();
        }

        private void UpdateSelectedMemberPathValuesBindings()
        {
            _selectedMemberPathValuesHelper.UpdateValueSource(ItemsCollection, SelectedMemberPath);
        }

        private void UpdateValueMemberPathValuesBindings()
        {
            _valueMemberPathValuesHelper.UpdateValueSource(ItemsCollection, ValueMemberPath);
        }

        /// <summary>
        /// This method will be called when the "IsSelected" property of an SelectorItem
        /// has been modified.
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnItemSelectionChanged(ItemSelectionChangedEventArgs args)
        {
            if (_surpressItemSelectionChanged)
                return;

            RaiseEvent(args);

            if (Command != null)
                Command.Execute(args.Item);
        }

        /// <summary>
        /// Updates the SelectedValue property based on what is present in the SelectedItems property.
        /// </summary>
        private void UpdateSelectedValue()
        {
            string newValue = string.Join(Delimiter, SelectedItems.Cast<object>().Select(x => GetItemValue(x)));

            if (string.IsNullOrEmpty(SelectedValue) || !SelectedValue.Equals(newValue))
            {
                _ignoreSelectedValueChanged = true;
                SelectedValue = newValue;
                _ignoreSelectedValueChanged = false;
            }
        }

        /// <summary>
        /// Updates the SelectedValue property based on what is present in the SelectedItems property.
        /// </summary>
        private void UpdateSelectedValues()
        {
            var values = new List<object>();

            foreach (var item in SelectedItems)
            {
                var value = GetItemValue(item);
                values.Add(value);
            }

            _ignoreSelectedValuesChanged = true;
            SelectedValues = values;
            _ignoreSelectedValuesChanged = false;
        }

        /// <summary>
        /// Updates the SelectedItem property based on what is present in the SelectedItems property.
        /// </summary>
        private void UpdateSelectedItem()
        {
            if (!SelectedItems.Contains(SelectedItem))
            {
                _ignoreSelectedItemChanged = true;
                SelectedItem = (SelectedItems.Count > 0) ? SelectedItems[0] : null;
                _ignoreSelectedItemChanged = false;
            }
        }

        /// <summary>
        /// Update the SelectedItems collection based on the values
        /// refered to by the SelectedMemberPath property.
        /// </summary>
        private void UpdateFromSelectedMemberPathValues()
        {
            _ignoreSelectedItemsCollectionChanged++;
            foreach (var item in ItemsCollection)
            {
                bool? isSelected = GetSelectedMemberPathValue(item);
                if (isSelected != null)
                {
                    if (isSelected.Value)
                    {
                        if (!SelectedItems.Contains(item))
                        {
                            SelectedItems.Add(item);
                        }
                    }
                    else
                    {
                        if (SelectedItems.Contains(item))
                        {
                            SelectedItems.Remove(item);
                        }
                    }
                }
            }
            _ignoreSelectedItemsCollectionChanged--;
            UpdateFromSelectedItems();
        }

        internal void UpdateSelectedItems(IList selectedItems)
        {
            if (selectedItems == null)
                throw new ArgumentNullException("selectedItems");

            // Just check if the collection is the same..
            if (selectedItems.Count == SelectedItems.Count
              && selectedItems.Cast<object>().SequenceEqual(SelectedItems.Cast<object>()))
                return;

            _ignoreSelectedItemsCollectionChanged++;
            SelectedItems.Clear();
            foreach (object newItem in selectedItems)
            {
                SelectedItems.Add(newItem);
            }
            _ignoreSelectedItemsCollectionChanged--;
            UpdateFromSelectedItems();
        }

        /// <summary>
        /// Updates the following based on the content of SelectedItems:
        /// - All SelectorItems "IsSelected" properties
        /// - Values refered to by SelectedMemberPath
        /// - SelectedItem property
        /// - SelectedValue property
        /// Refered to by the SelectedMemberPath property.
        /// </summary>
        private void UpdateFromSelectedItems()
        {
            foreach (object o in ItemsCollection)
            {
                bool isSelected = SelectedItems.Contains(o);

                _ignoreSelectedMemberPathValuesChanged++;
                SetSelectedMemberPathValue(o, isSelected);
                _ignoreSelectedMemberPathValuesChanged--;

                var selectorItem = ItemContainerGenerator.ContainerFromItem(o) as SelectorItem;
                if (selectorItem != null)
                {
                    selectorItem.IsSelected = isSelected;
                }
            }

            UpdateSelectedItem();
            UpdateSelectedValue();
            UpdateSelectedValues();
        }

        /// <summary>
        /// Removes all items from SelectedItems that are no longer in ItemsSource.
        /// </summary>
        private void RemoveUnavailableSelectedItems()
        {
            _ignoreSelectedItemsCollectionChanged++;
            HashSet<object> hash = new HashSet<object>(ItemsCollection.Cast<object>());

            for (int i = 0; i < SelectedItems.Count; i++)
            {
                if (!hash.Contains(SelectedItems[i]))
                {
                    _removedItems.Add(SelectedItems[i]);
                    SelectedItems.RemoveAt(i);
                    i--;
                }
            }
            _ignoreSelectedItemsCollectionChanged--;

            UpdateSelectedItem();
            UpdateSelectedValue();
            UpdateSelectedValues();
        }

        private void AddAvailableRemovedItems()
        {
            HashSet<object> hash = new HashSet<object>(ItemsCollection.Cast<object>());

            for (int i = 0; i < _removedItems.Count; i++)
            {
                if (hash.Contains(_removedItems[i]))
                {
                    SelectedItems.Add(_removedItems[i]);
                    _removedItems.RemoveAt(i);
                    i--;
                }
            }
        }

        /// <summary>
        /// Updates the SelectedItems collection based on the content of
        /// the SelectedValue property.
        /// </summary>
        private void UpdateFromSelectedValue()
        {
            List<string> selectedValues = null;
            if (!String.IsNullOrEmpty(SelectedValue))
            {
                selectedValues = SelectedValue.Split(new[] { Delimiter }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }

            UpdateFromList(selectedValues, GetItemValue);
        }

        #endregion Methods

        #region IWeakEventListener Members

        public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            if (managerType == typeof(CollectionChangedEventManager))
            {
                if (ReferenceEquals(_selectedItems, sender))
                {
                    OnSelectedItemsCollectionChanged(sender, (NotifyCollectionChangedEventArgs)e);
                    return true;
                }
                else if (ReferenceEquals(ItemsCollection, sender))
                {
                    OnItemsSourceCollectionChanged(sender, (NotifyCollectionChangedEventArgs)e);
                    return true;
                }
            }

            return false;
        }

        #endregion IWeakEventListener Members

        #region ValueEqualityComparer private class

        private class ValueEqualityComparer : IEqualityComparer<string>
        {
            public bool Equals(string x, string y)
            {
                return string.Equals(x, y, StringComparison.InvariantCultureIgnoreCase);
            }

            public int GetHashCode(string obj)
            {
                return 1;
            }
        }

        #endregion ValueEqualityComparer private class
    }

    public delegate void ItemSelectionChangedEventHandler(object sender, ItemSelectionChangedEventArgs e);

    public class ItemSelectionChangedEventArgs : RoutedEventArgs
    {
        public bool IsSelected
        {
            get;
            private set;
        }

        public object Item
        {
            get;
            private set;
        }

        public ItemSelectionChangedEventArgs(RoutedEvent routedEvent, object source, object item, bool isSelected)
          : base(routedEvent, source)
        {
            Item = item;
            IsSelected = isSelected;
        }
    }
}