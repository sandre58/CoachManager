using System.Windows;
using System.Windows.Interactivity;

namespace My.CoachManager.Presentation.Wpf.Controls.Behaviours
{
    public class StylizedBehaviorCollection : FreezableCollection<Behavior>
    {
        protected override Freezable CreateInstanceCore()
        {
            return new StylizedBehaviorCollection();
        }
    }
}