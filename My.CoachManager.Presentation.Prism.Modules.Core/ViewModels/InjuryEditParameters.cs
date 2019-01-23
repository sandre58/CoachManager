using System;
using My.CoachManager.Presentation.Prism.Core.ViewModels;

namespace My.CoachManager.Presentation.Prism.Modules.Core.ViewModels
{
    public class InjuryEditParameters : ItemParameters
    {
        public int PlayerId { get; set; }

        public DateTime Date { get; set; }

        public InjuryEditParameters(int id) : base(id)
        {
        }
    }
}
