using System.Windows;
using My.CoachManager.Presentation.Controls.TimePickers;

namespace My.CoachManager.Presentation.Controls
{
    /// <summary>
    ///     Represents a control that allows the user to select a time.
    /// </summary>
    public class ExtendedTimePicker : TimePickerBase
    {
        static ExtendedTimePicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ExtendedTimePicker), new FrameworkPropertyMetadata(typeof(ExtendedTimePicker)));
        }

        public ExtendedTimePicker()
        {
            IsDatePickerVisible = false;
        }
    }
}