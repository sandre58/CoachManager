using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using My.CoachManager.Presentation.Prism.Controls.TimePickers;

namespace My.CoachManager.Presentation.Prism.Controls
{
    public class TimePicker : TimePickerBase
    {
        static TimePicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TimePicker), new FrameworkPropertyMetadata(typeof(TimePicker)));
        }

        public TimePicker()
        {
            IsDatePickerVisible = false;
        }

        protected override void OnTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            TimeSpan ts;
            if (TimeSpan.TryParse(((DatePickerTextBox)sender).Text, SpecificCultureInfo, out ts))
            {
                SetCurrentValue(SelectedTimeProperty, SelectedTime.GetValueOrDefault() + ts);
            }
            else
            {
                //WriteValueToTextBox(GetValueForTextBox());
                if (string.IsNullOrEmpty(((DatePickerTextBox)sender).Text))
                {
                    SetCurrentValue(SelectedTimeProperty, null);
                    //WriteValueToTextBox(GetValueForTextBox());
                }
                else
                {
                    WriteValueToTextBox(GetValueForTextBox());
                }
            }
        }
    }
}
