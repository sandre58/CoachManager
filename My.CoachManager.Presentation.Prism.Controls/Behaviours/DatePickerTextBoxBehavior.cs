using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Interactivity;
using System.Windows.Threading;
using My.CoachManager.Presentation.Prism.Controls.Extensions;
using My.CoachManager.Presentation.Prism.Controls.Parameters;

namespace My.CoachManager.Presentation.Prism.Controls.Behaviours
{
    public class DatePickerTextBoxBehavior : Behavior<DatePickerTextBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.TextChanged += OnTextChanged;
            this.BeginInvoke(SetHasTextProperty, DispatcherPriority.Loaded);
        }

        protected override void OnDetaching()
        {
            AssociatedObject.TextChanged -= OnTextChanged;
            base.OnDetaching();
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            SetHasTextProperty();
        }

        private void SetHasTextProperty()
        {
            AssociatedObject.TemplatedParent?.SetValue(TextBoxParameters.HasTextProperty, AssociatedObject.Text.Length > 0);
        }
    }
}