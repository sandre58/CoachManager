using System.Windows.Input;

namespace My.CoachManager.Presentation.Controls.Schedulers
{
    internal static class SchedulerKeyboardHelper
    {
        public static void GetMetaKeyState(out bool ctrl, out bool shift)
        {
            ctrl = (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control;
            shift = (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift;
        }
    }
}
