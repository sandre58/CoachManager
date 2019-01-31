using System.Windows.Input;

namespace My.CoachManager.Presentation.Controls.Helpers
{
    public static class KeyboardHelper
    {
        public static bool IsKeyModifyingPopupState(KeyEventArgs e)
        {
            return (System.Windows.Input.Keyboard.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt && (e.SystemKey == Key.Down || e.SystemKey == Key.Up)
                   || e.Key == Key.F4;
        }
    }
}
