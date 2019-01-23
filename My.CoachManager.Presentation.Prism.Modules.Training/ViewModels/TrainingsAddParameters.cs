using System;

namespace My.CoachManager.Presentation.Prism.Modules.Training.ViewModels
{
    public class TrainingsAddParameters : TrainingEditParameters
    {
        /// <summary>
        /// Gets or sets defaultdate.
        /// </summary>
        public DateTime? EndDate { get; set; }

        public TrainingsAddParameters(int id) : base(id)
        {
        }
    }
}
