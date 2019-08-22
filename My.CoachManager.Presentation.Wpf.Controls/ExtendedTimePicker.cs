using System.Windows;
using My.CoachManager.Presentation.Wpf.Controls.TimePickers;

namespace My.CoachManager.Presentation.Wpf.Controls
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