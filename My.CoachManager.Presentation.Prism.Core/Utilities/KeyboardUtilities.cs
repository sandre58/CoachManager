using System.Windows.Input;

namespace My.CoachManager.Presentation.Prism.Core.Utilities
{
    public class KeyboardUtilities
    {
        public static bool IsKeyModifyingPopupState(KeyEventArgs e)
        {
            return ((((Keyboard.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt) && ((e.SystemKey == Key.Down) || (e.SystemKey == Key.Up)))
                    || (e.Key == Key.F4));
        }
    }
}