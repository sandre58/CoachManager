using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace My.CoachManager.Domain.Entities
{
    /// <summary>
    /// Provides properties for a training Entity.
    /// </summary>
    [Serializable]
    public class Training : Entity
    {
        /// <summary>
        /// Initalize a new instance of <see cref="Player"/>.
        /// </summary>
        public Training()
        {
            Attendances = new List<TrainingAttendance>();
            Exercices = new List<Exercice>();
            Stage = ExerciceConstants.DefaultStage;
        }

        /// <summary>
        /// Gets or sets the roster id.
        /// </summary>
        [Required]
        public int? RosterId { get; set; }

        /// <summary>
        /// Gets or sets the roster.
        /// </summary>
        public virtual Roster Roster { get; set; }

        /// <summary>
        /// Gets or sets the place.
        /// </summary>
        public virtual string Place { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        [Required]
        public virtual DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        [Required]
        public virtual DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the is cancelled value.
        /// </summary>
        public virtual bool IsCancelled { get; set; }

        /// <summary>
        /// Gets or sets the theme.
        /// </summary>
        public virtual string Theme { get; set; }

        /// <summary>
        /// Gets or sets the stage.
        /// </summary>
        public Stage Stage { get; set; }

        /// <summary>
        /// Gets or sets the training attendances.
        /// </summary>
        public ICollection<TrainingAttendance> Attendances { get; set; }

        /// <summary>
        /// Gets or sets the training exercices.
        /// </summary>
        public ICollection<Exercice> Exercices { get; set; }
    }
}