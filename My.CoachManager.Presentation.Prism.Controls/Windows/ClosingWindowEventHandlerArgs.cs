using System;

namespace My.CoachManager.Presentation.Prism.Controls.Windows
{
    public class ClosingWindowEventHandlerArgs : EventArgs
    {
        public bool Cancelled { get; set; }
    }
}