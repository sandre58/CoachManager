using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.Models;

namespace My.CoachManager.Presentation.Prism.Modules.Roster.ViewModels
{
    public partial class RosterPlayerEditViewModel : EditViewModel<RosterPlayerModel>
    {
        protected override bool SaveItemCore()
        {
            return true;
        }

        protected override RosterPlayerModel LoadItemCore(int id)
        {
            return null;
        }
    }
}