using System.Windows.Automation.Peers;

namespace My.CoachManager.Presentation.Prism.Controls.CommandButtons
{
    public abstract class CommandButtonBaseAutomationPeer : ButtonBaseAutomationPeer
    {
        protected CommandButtonBaseAutomationPeer(CommandButtonBase owner) : base(owner)
        {
        }

        public new CommandButtonBase Owner
        {
            get
            {
                var result = (CommandButtonBase)base.Owner;

                return result;
            }
        }
        
        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            return AutomationControlType.Button;
        }
    }
}
