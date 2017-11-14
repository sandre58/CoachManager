using System.Collections.Generic;

namespace My.CoachManager.Presentation.Core.ViewModels.Screens
{
    public class MenuItemViewModel : BaseViewModel
    {
        #region Fields

        private string _title;

        private object _icon;

        private bool _isSelected;

        private bool _isEnabled;

        private readonly MenuItemViewModel _parent;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// default constructor.
        /// </summary>
        /// <param name="title">Title.</param>
        /// /// <param name="parent">Parent.</param>
        public MenuItemViewModel(string title, MenuItemViewModel parent = null)
        {
            IsEnabled = true;
            Title = title;
            _parent = parent;
            SubItems = new List<MenuItemViewModel>();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Get or set the title.
        /// </summary>
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                SetEntityProperty(() => _title = value, value, _title);
            }
        }

        /// <summary>
        /// Get or set the icon.
        /// </summary>
        public object Icon
        {
            get
            {
                return _icon;
            }
            set
            {
                SetEntityProperty(() => _icon = value, value, _icon);
            }
        }

        /// <summary>
        /// Get or set the view model.
        /// </summary>
        public MenuItemViewModel Parent
        {
            get
            {
                return _parent;
            }
        }

        /// <summary>
        /// Get or set if is selected.
        /// </summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                SetEntityProperty(() =>
                {
                    _isSelected = value;
                    if (Parent != null && value) Parent.IsSelected = value;
                }, value, _isSelected);
            }
        }

        /// <summary>
        /// Get or set if is selected.
        /// </summary>
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                SetEntityProperty(() =>
                {
                    _isEnabled = value;
                    if (Parent != null && value) Parent.IsEnabled = value;
                }, value, _isSelected);
            }
        }

        /// <summary>
        /// Get or set the sub menus.
        /// </summary>
        public virtual ICollection<MenuItemViewModel> SubItems { get; private set; }

        #endregion Properties
    }

    public class MenuItemViewModel<T> : MenuItemViewModel
    {
        #region Fields

        private T _target;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// default constructor.
        /// </summary>
        /// <param name="title">Title.</param>
        /// ///
        /// <param name="target"></param>
        /// <param name="parent">Parent.</param>
        public MenuItemViewModel(string title, T target, MenuItemViewModel parent = null)
            : base(title, parent)
        {
            _target = target;
            SubItems = new List<MenuItemViewModel<T>>();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Get or set the target.
        /// </summary>
        public T Target
        {
            get
            {
                return _target;
            }
            set
            {
                SetEntityProperty(() => _target = value, value, _target);
            }
        }

        /// <summary>
        /// Get or set the sub menus.
        /// </summary>
        public new virtual ICollection<MenuItemViewModel<T>> SubItems { get; private set; }

        #endregion Properties
    }
}