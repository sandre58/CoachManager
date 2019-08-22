using System;

namespace My.CoachManager.Presentation.Wpf.Controls.Windows
{
    public class ClosingWindowEventHandlerArgs : EventArgs
    {
        public bool Cancelled { get; set; }
    }
}