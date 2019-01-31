using System;

namespace My.CoachManager.Presentation.Core.DragAndDrop
{
    public class DragAndDropEventArgs : EventArgs
    {
        public object Source { get; set; }
        public object Target { get; set; }

        public DragAndDropEventArgs(object source, object target)
        {
            Source = source;
            Target = target;
        }
    }
}