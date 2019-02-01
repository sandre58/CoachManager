using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.Domain.Core;

namespace My.CoachManager.Domain.Entities
{
    /// <summary>
    /// Provides properties for a training Entity.
    /// </summary>
    public class ExerciceTemplate : Entity
    {
        /// <summary>
        /// Initalize a new instance of <see cref="ExerciceTemplate"/>.
        /// </summary>
        public ExerciceTemplate()
        {
            Goals = new List<string>();
            Instructions = new List<string>();
            Variables = new List<string>();
            Methods = new List<string>();
            Stages = new List<Stage>();
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        [Required]
        public ExerciceType Type { get; set; }

        /// <summary>
        /// Gets or sets the theme.
        /// </summary>
        public virtual string Theme { get; set; }

        /// <summary>
        /// Gets or sets the stage.
        /// </summary>
        public ICollection<Stage> Stages { get; set; }

        /// <summary>
        /// Gets or sets the objective.
        /// </summary>
        public string Objective { get; set; }

        /// <summary>
        /// Gets or sets the goals.
        /// </summary>
        public ICollection<string> Goals { get; set; }

        /// <summary>
        /// Gets or sets the instructions.
        /// </summary>
        public ICollection<string> Instructions { get; set; }

        /// <summary>
        /// Gets or sets the variables.
        /// </summary>
        public ICollection<string> Variables { get; set; }

        /// <summary>
        /// Gets or sets the variables.
        /// </summary>
        public ICollection<string> Methods { get; set; }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        public int Duration { get; set; }
    }
}