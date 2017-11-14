using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using My.CoachManager.Presentation.Core.Helpers;

namespace My.CoachManager.Presentation.Core.Behaviors
{
    public class DragDropRowBehavior : Behavior<DataGrid>
    {
        private object _draggedItem;
        private bool _isEditing;
        private bool _isDragging;

        #region DragEnded

        public static readonly DependencyProperty DragEndedProperty = DependencyProperty.Register("DragEnded",
            typeof(ICommand), typeof(DragDropRowBehavior));

        public ICommand DragEnded
        {
            get { return (ICommand)GetValue(DragEndedProperty); }
            set { SetValue(DragEndedProperty, value); }
        }

        private void RaiseDragEndedEvent()
        {
            DragEnded.Execute(null);
        }

        #endregion DragEnded

        #region Popup

        public static readonly DependencyProperty PopupProperty =
            DependencyProperty.Register("Popup", typeof(System.Windows.Controls.Primitives.Popup),
                typeof(DragDropRowBehavior));

        public System.Windows.Controls.Primitives.Popup Popup
        {
            get { return (System.Windows.Controls.Primitives.Popup)GetValue(PopupProperty); }
            set { SetValue(PopupProperty, value); }
        }

        #endregion Popup

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.BeginningEdit += OnBeginEdit;
            AssociatedObject.CellEditEnding += OnEndEdit;
            AssociatedObject.MouseLeftButtonUp += OnMouseLeftButtonUp;
            AssociatedObject.PreviewMouseLeftButtonDown += OnMouseLeftButtonDown;
            AssociatedObject.MouseMove += OnMouseMove;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.BeginningEdit -= OnBeginEdit;
            AssociatedObject.CellEditEnding -= OnEndEdit;
            AssociatedObject.MouseLeftButtonUp -= OnMouseLeftButtonUp;
            AssociatedObject.MouseLeftButtonDown -= OnMouseLeftButtonDown;
            AssociatedObject.MouseMove -= OnMouseMove;

            Popup = null;
            _draggedItem = null;
            _isEditing = false;
            _isDragging = false;
        }

        private void OnBeginEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            _isEditing = true;
            //in case we are in the middle of a drag/drop operation, cancel it...
            if (_isDragging) ResetDragDrop();
        }

        private void OnEndEdit(object sender, DataGridCellEditEndingEventArgs e)
        {
            _isEditing = false;
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_isEditing) return;

            var row = UiHelper.TryFindFromPoint<DataGridRow>((UIElement)sender, e.GetPosition(AssociatedObject));
            if (row == null || row.IsEditing) return;

            //set flag that indicates we're capturing mouse movements
            _isDragging = true;
            _draggedItem = row.Item;
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!_isDragging || _isEditing)
                return;

            RaiseDragEndedEvent();

            //reset
            ResetDragDrop();
        }

        private void ResetDragDrop()
        {
            _isDragging = false;
            Popup.IsOpen = false;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDragging || e.LeftButton != MouseButtonState.Pressed)
                return;

            Popup.DataContext = _draggedItem;
            //display the popup if it hasn't been opened yet
            if (!Popup.IsOpen)
            {
                //make sure the popup is visible
                Popup.IsOpen = true;
            }

            var popupSize = new Size(Popup.ActualWidth, Popup.ActualHeight);
            Popup.PlacementRectangle = new Rect(e.GetPosition(AssociatedObject), popupSize);

            //get the target item
            var row = UiHelper.TryFindFromPoint<DataGridRow>((UIElement)sender, e.GetPosition(AssociatedObject));

            if (row != null)
            {
                var targetItem = row.Item;

                //get target index
                var list = (AssociatedObject).ItemsSource as IList;
                if (list != null)
                {
                    var targetIndex = list.IndexOf(targetItem);

                    if (targetIndex > -1 && targetIndex < list.Count)
                    {
                        //remove the source from the list
                        list.Remove(_draggedItem);

                        //move source at the target's location
                        list.Insert(targetIndex, _draggedItem);

                        AssociatedObject.SelectedItem = _draggedItem;
                    }
                }
            }
        }
    }
}