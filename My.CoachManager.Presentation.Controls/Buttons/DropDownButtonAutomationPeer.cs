using System;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;

namespace My.CoachManager.Presentation.Controls.Buttons
{
    public class DropDownButtonAutomationPeer : ButtonBaseAutomationPeer, IExpandCollapseProvider
    {
        public DropDownButtonAutomationPeer(DropDownButton owner) : base(owner)
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
            var owner = (DropDownButton)Owner;
            owner.IsDropDownOpen = true;
        }
        
        public void Collapse()
        {
            IsEnabledAndHasSubmenu();
            var owner = (DropDownButton)Owner;
            owner.IsDropDownOpen = false;
        }
        
        private void IsEnabledAndHasSubmenu()
        {
            if (!IsEnabled())
            {
                throw new ElementNotEnabledException();
            }
            if (!((DropDownButton)Owner).HasSubmenu)
            {
                throw new InvalidOperationException("Operation can't be perform");
            }
        }
        
        public ExpandCollapseState ExpandCollapseState
        {
            get
            {
                var owner = (DropDownButton)Owner;
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
