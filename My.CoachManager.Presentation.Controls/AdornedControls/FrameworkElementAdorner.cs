using System.Collections;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace My.CoachManager.Presentation.Controls.AdornedControls
{
    /// <summary>
    /// This class is an adorner that allows a FrameworkElement derived class to adorn another FrameworkElement.
    /// </summary>
    public class FrameworkElementAdorner : Adorner
    {
        #region Fields

        private readonly FrameworkElement _child;

        private readonly AdornerPlacement _horizontalAdornerPlacement = AdornerPlacement.Inside;
        private readonly AdornerPlacement _verticalAdornerPlacement = AdornerPlacement.Inside;

        private readonly double _offsetX;
        private readonly double _offsetY;

        private double _positionX = double.NaN;
        private double _positionY = double.NaN;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialises a new instance of the <see cref="FrameworkElementAdorner"/> class.
        /// </summary>
        /// <param name="adornerChildElement">The adorner child element.</param>
        /// <param name="adornedElement">The adorned element.</param>
        public FrameworkElementAdorner(
            FrameworkElement adornerChildElement,
            FrameworkElement adornedElement)
            : base(adornedElement)
        {
            _child = adornerChildElement;

            AddLogicalChild(adornerChildElement);
            AddVisualChild(adornerChildElement);
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="FrameworkElementAdorner"/> class.
        /// </summary>
        /// <param name="adornerChildElement">The adorner child element.</param>
        /// <param name="adornedElement">The adorned element.</param>
        /// <param name="horizontalAdornerPlacement">The horizontal adorner placement.</param>
        /// <param name="verticalAdornerPlacement">The vertical adorner placement.</param>
        /// <param name="offsetX">The offset X.</param>
        /// <param name="offsetY">The offset Y.</param>
        public FrameworkElementAdorner(
            FrameworkElement adornerChildElement,
            FrameworkElement adornedElement,
            AdornerPlacement horizontalAdornerPlacement,
            AdornerPlacement verticalAdornerPlacement,
            double offsetX,
            double offsetY)
            : base(adornedElement)
        {
            _child = adornerChildElement;
            _horizontalAdornerPlacement = horizontalAdornerPlacement;
            _verticalAdornerPlacement = verticalAdornerPlacement;
            _offsetX = offsetX;
            _offsetY = offsetY;

            adornedElement.SizeChanged += OnAdornedElementSizeChanged;

            AddLogicalChild(adornerChildElement);
            AddVisualChild(adornerChildElement);
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets the <see cref="FrameworkElement"/> that this adorner is bound to.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// The element that this adorner is bound to.
        /// The default value is null.
        /// </returns>
        public new FrameworkElement AdornedElement
        {
            get
            {
                return (FrameworkElement)base.AdornedElement;
            }
        }

        /// <summary>
        /// Gets or sets the X position of the child (when not set to NaN).
        /// </summary>
        /// <value>The X position.</value>
        public double PositionX
        {
            get { return _positionX; }
            set { _positionX = value; }
        }

        /// <summary>
        /// Gets or sets the Y position of the child (when not set to NaN).
        /// </summary>
        /// <value>The Y position.</value>
        public double PositionY
        {
            get { return _positionY; }
            set { _positionY = value; }
        }

        #endregion Public Properties

        #region Protected Properties

        /// <summary>
        /// Gets an enumerator for logical child elements of this element.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// An enumerator for logical child elements of this element.
        /// </returns>
        protected override IEnumerator LogicalChildren
        {
            get
            {
                ArrayList list = new ArrayList();
                list.Add(_child);
                return list.GetEnumerator();
            }
        }

        /// <summary>
        /// Gets the number of visual child elements within this element.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// The number of visual child elements for this element.
        /// </returns>
        protected override int VisualChildrenCount
        {
            get { return 1; }
        }

        #endregion Protected Properties

        #region Public Methods

        /// <summary>
        /// Disconnect the child element from the visual tree so that it may be reused later.
        /// </summary>
        public void DisconnectChild()
        {
            RemoveLogicalChild(_child);
            RemoveVisualChild(_child);
        }

        #endregion Public Methods

        #region Protected Methods

        /// <summary>
        /// When overridden in a derived class, positions child elements and determines a size for a <see cref="T:System.Windows.FrameworkElement"/> derived class.
        /// </summary>
        /// <param name="finalSize">The final area within the parent that this element should use to arrange itself and its children.</param>
        /// <returns>The actual size used.</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            double x = PositionX;
            if (double.IsNaN(x))
            {
                x = DetermineX();
            }

            double y = PositionY;
            if (double.IsNaN(y))
            {
                y = DetermineY();
            }

            double adornerWidth = DetermineWidth();
            double adornerHeight = DetermineHeight();
            _child.Arrange(new Rect(x, y, adornerWidth, adornerHeight));
            return finalSize;
        }

        /// <summary>
        /// Overrides <see cref="M:System.Windows.Media.Visual.GetVisualChild(System.Int32)"/>, and returns a child at the specified index from a collection of child elements.
        /// </summary>
        /// <param name="index">The zero-based index of the requested child element in the collection.</param>
        /// <returns>
        /// The requested child element. This should not return null; if the provided index is out of range, an exception is thrown.
        /// </returns>
        protected override Visual GetVisualChild(int index)
        {
            return _child;
        }

        /// <summary>
        /// Implements any custom measuring behaviour for the adorner.
        /// </summary>
        /// <param name="constraint">A size to constrain the adorner to.</param>
        /// <returns>
        /// A <see cref="T:System.Windows.Size"/> object representing the amount of layout space needed by the adorner.
        /// </returns>
        protected override Size MeasureOverride(Size constraint)
        {
            _child.Measure(constraint);
            return _child.DesiredSize;
        }

        #endregion Protected Methods

        #region Private Methods

        /// <summary>
        /// Determine the X coordinate of the child.
        /// </summary>
        /// <returns>The X coordinate of the child.</returns>
        private double DetermineX()
        {
            switch (_child.HorizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    {
                        if (_horizontalAdornerPlacement == AdornerPlacement.Outside)
                        {
                            return -_child.DesiredSize.Width + _offsetX;
                        }
                        else
                        {
                            return _offsetX;
                        }
                    }

                case HorizontalAlignment.Right:
                    {
                        if (_horizontalAdornerPlacement == AdornerPlacement.Outside)
                        {
                            double adornedWidth = AdornedElement.ActualWidth;
                            return adornedWidth + _offsetX;
                        }
                        else
                        {
                            double adornerWidth = _child.DesiredSize.Width;
                            double adornedWidth = AdornedElement.ActualWidth;
                            double x = adornedWidth - adornerWidth;
                            return x + _offsetX;
                        }
                    }

                case HorizontalAlignment.Center:
                    {
                        double adornerWidth = _child.DesiredSize.Width;
                        double adornedWidth = AdornedElement.ActualWidth;
                        double x = (adornedWidth / 2) - (adornerWidth / 2);
                        return x + _offsetX;
                    }

                case HorizontalAlignment.Stretch:
                    {
                        return 0.0;
                    }
            }

            return 0.0;
        }

        /// <summary>
        /// Determine the Y coordinate of the child.
        /// </summary>
        /// <returns>The Y coordinate of the child.</returns>
        private double DetermineY()
        {
            switch (_child.VerticalAlignment)
            {
                case VerticalAlignment.Top:
                    {
                        if (_verticalAdornerPlacement == AdornerPlacement.Outside)
                        {
                            return -_child.DesiredSize.Height + _offsetY;
                        }
                        else
                        {
                            return _offsetY;
                        }
                    }

                case VerticalAlignment.Bottom:
                    {
                        if (_verticalAdornerPlacement == AdornerPlacement.Outside)
                        {
                            double adornedHeight = AdornedElement.ActualHeight;
                            return adornedHeight + _offsetY;
                        }
                        else
                        {
                            double adornerHeight = _child.DesiredSize.Height;
                            double adornedHeight = AdornedElement.ActualHeight;
                            double x = adornedHeight - adornerHeight;
                            return x + _offsetY;
                        }
                    }

                case VerticalAlignment.Center:
                    {
                        double adornerHeight = _child.DesiredSize.Height;
                        double adornedHeight = AdornedElement.ActualHeight;
                        double x = (adornedHeight / 2) - (adornerHeight / 2);
                        return x + _offsetY;
                    }

                case VerticalAlignment.Stretch:
                    {
                        return 0.0;
                    }
            }

            return 0.0;
        }

        /// <summary>
        /// Determine the width of the child.
        /// </summary>
        /// <returns>The width of the child.</returns>
        private double DetermineWidth()
        {
            if (!double.IsNaN(PositionX))
            {
                return _child.DesiredSize.Width;
            }

            switch (_child.HorizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    return _child.DesiredSize.Width;

                case HorizontalAlignment.Right:
                    return _child.DesiredSize.Width;

                case HorizontalAlignment.Center:
                    return _child.DesiredSize.Width;

                case HorizontalAlignment.Stretch:
                    return AdornedElement.ActualWidth;
            }

            return 0.0;
        }

        /// <summary>
        /// Determine the height of the child.
        /// </summary>
        /// <returns>The height of the child.</returns>
        private double DetermineHeight()
        {
            if (!double.IsNaN(PositionY))
            {
                return _child.DesiredSize.Height;
            }

            switch (_child.VerticalAlignment)
            {
                case VerticalAlignment.Top:
                    return _child.DesiredSize.Height;

                case VerticalAlignment.Bottom:
                    return _child.DesiredSize.Height;

                case VerticalAlignment.Center:
                    return _child.DesiredSize.Height;

                case VerticalAlignment.Stretch:
                    return AdornedElement.ActualHeight;
            }

            return 0.0;
        }

        /// <summary>
        /// Event raised when the adorned control's size has changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.SizeChangedEventArgs"/> instance containing the event data.</param>
        private void OnAdornedElementSizeChanged(object sender, SizeChangedEventArgs e)
        {
            InvalidateMeasure();
        }

        #endregion Private Methods
    }
}