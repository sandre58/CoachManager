using System;
using System.Windows.Input;

namespace My.CoachManager.Presentation.Prism.Core.Interactivity
{
    public class KeyDownItemEventArgs : EventArgs
    {
        public KeyEventArgs EventArgs { get; set; }
        public object Item { get; set; }

        public KeyDownItemEventArgs(KeyEventArgs eventArgs, object item)
        {
            EventArgs = eventArgs;
            Item = item;
        }
    }
}