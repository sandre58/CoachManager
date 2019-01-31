    using System.Windows.Automation.Peers;

namespace My.CoachManager.Presentation.Controls.SubMenus
{
    public class SubmenuAutomationPeer : FrameworkElementAutomationPeer
    {
        public SubmenuAutomationPeer(Submenu owner) : base(owner)
        {
        }
        
        [System.Diagnostics.Contracts.Pure]
        protected override string GetClassNameCore()
        {
            return "Submenu";
        }
        
        [System.Diagnostics.Contracts.Pure]
        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            return AutomationControlType.Menu;
        }
    }
}
