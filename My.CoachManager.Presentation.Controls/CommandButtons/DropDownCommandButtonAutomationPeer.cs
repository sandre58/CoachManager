using System;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;

namespace My.CoachManager.Presentation.Controls.CommandButtons
{
    public class DropDownCommandButtonAutomationPeer : CommandButtonBaseAutomationPeer, IExpandCollapseProvider
    {
        public DropDownCommandButtonAutomationPeer(DropDownCommandButton owner) : base(owner)
        {
        }
        
        [System.Diagnostics.Contracts.Pure]
        protected override string GetClassNameCore()
        {
            return "DropDownCommandButton";
        }
        
        public override object GetPattern(PatternInterface patternInterface)
        {
            return patternInterface == PatternInterface.ExpandCollapse ? this : base.GetPattern(patternInterface);
        }
        
        public void Expand()
        {
            IsEnabledAndHasSubmenu();
            var owner = (DropDownCommandButton)Owner;
            owner.IsDropDownOpen = true;
        }
        
        public void Collapse()
        {
            IsEnabledAndHasSubmenu();
            var owner = (DropDownCommandButton)Owner;
            owner.IsDropDownOpen = false;
        }
        
        private void IsEnabledAndHasSubmenu()
        {
            if (!IsEnabled())
            {
                throw new ElementNotEnabledException();
            }
            if (!((DropDownCommandButton)Owner).HasSubmenu)
            {
                throw new InvalidOperationException("Operation can't be perform");
            }
        }
        
        public ExpandCollapseState ExpandCollapseState
        {
            get
            {
                var owner = (DropDownCommandButton)Owner;
                return !owner.HasSubmenu ? ExpandCollapseState.LeafNode : (owner.IsDropDownOpen ? ExpandCollapseState.Expanded : ExpandCollapseState.Collapsed);
            }
        }
        
        internal void RaiseExpandCollapseStatePropertyChangedEvent(ExpandCollapseState oldValue, ExpandCollapseState newValue)
        {
            if (oldValue != newValue)
            {
                RaisePropertyChangedEvent(ExpandCollapsePatternIdentifiers.ExpandCollapseStateProperty, oldValue, newValue);
            }
        }
    }
}
