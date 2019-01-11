using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Threading;

namespace My.CoachManager.Presentation.Prism.Controls.CommandButtons
{
    public class CommandButtonAutomationPeer : CommandButtonBaseAutomationPeer, IInvokeProvider
    {
        public CommandButtonAutomationPeer(CommandButton owner) : base(owner)
        {
        }
        
        protected override string GetClassNameCore()
        {
            return "CommandButton";
        }

        public override object GetPattern(PatternInterface patternInterface)
        {
            return patternInterface == PatternInterface.Invoke ? this : base.GetPattern(patternInterface);
        }
        
        public void Invoke()
        {

            Dispatcher.BeginInvoke(DispatcherPriority.Input, new DispatcherOperationCallback(
                delegate
                {
                    ((CommandButton)Owner).OnClickInternal();
                    return null;
                }), null);
        }
    }
}
