using System;

namespace My.CoachManager.Presentation.Prism.Core.Models
{
    public interface IAppointment
    {

        /// <summary>
        /// Gets or sets start date.
        /// </summary>
        DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets end date.
        /// </summary>
        DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or Sets Title.
        /// </summary>
        string Title { get; set; }
    }
}
