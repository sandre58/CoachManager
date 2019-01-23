using System;
using My.CoachManager.Presentation.Prism.Core.ViewModels;

namespace My.CoachManager.Presentation.Prism.Modules.Training.ViewModels
{
    public class TrainingEditParameters : ItemParameters
    {
        public DateTime? Date { get; set; }

        public TimeSpan StartTime { get; set; }


        public TimeSpan Duration { get; set; }

        public TrainingEditParameters(int id) : base(id)
        {
        }
    }
}
