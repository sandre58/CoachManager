using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace My.CoachManager.Presentation.Wpf.Controls
{
    public interface IThumb : IInputElement
    {
        event DragStartedEventHandler DragStarted;

        event DragDeltaEventHandler DragDelta;

        event DragCompletedEventHandler DragCompleted;

        event MouseButtonEventHandler MouseDoubleClick;
    }
}