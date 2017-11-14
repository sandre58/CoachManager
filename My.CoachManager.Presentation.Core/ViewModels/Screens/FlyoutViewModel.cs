using My.CoachManager.Presentation.Core.ViewModels.Screens.Enums;
using My.CoachManager.Presentation.Core.ViewModels.Screens.Interfaces;

namespace My.CoachManager.Presentation.Core.ViewModels.Screens
{
    public abstract class FlyoutViewModel : WorkspaceViewModel, IFlyoutViewModel
    {
        #region Fields

        private FlyoutVisibilityPosition _position;
        private bool _isOpen;
        private FlyoutTheme _theme;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the position of the flyout instance.
        /// </summary>
        public FlyoutVisibilityPosition Position
        {
            get { return _position; }
            set
            {
                if (_position == value)
                    return;
                _position = value;
                NotifyOfPropertyChange();
            }
        }

        /// <summary>
        /// Gets or sets if the flyout insatnce is visible or collapsed.
        /// </summary>
        public bool IsOpen
        {
            get { return _isOpen; }
            set
            {
                if (_isOpen == value)
                    return;
                _isOpen = value;
                NotifyOfPropertyChange();
            }
        }

        /// <summary>
        /// Gets or sets the theme of the flyout instance.
        /// </summary>
        public FlyoutTheme Theme
        {
            get { return _theme; }
            set
            {
                if (_theme == value)
                    return;
                _theme = value;
                NotifyOfPropertyChange();
            }
        }

        #endregion Properties
    }
}