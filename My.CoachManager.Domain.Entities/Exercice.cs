using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.Domain.Core;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace My.CoachManager.Domain.Entities
{
    /// <summary>
    /// Provides properties for a training Entity.
    /// </summary>
    public class Exercice : Entity, IOrderable
    {
        /// <summary>
        /// Initalize a new instance of <see cref="Exercice"/>.
        /// </summary>
        public Exercice()
        {
            Goals = new List<string>();
            Instructions = new List<string>();
            Variables = new List<string>();
            Methods = new List<string>();
            Type = ExerciceConstants.DefaultType;
        }

        /// <summary>
        /// Gets or sets the player's training id.
        /// </summary>
        [Required]
        public int TrainingId { get; set; }

        /// <summary>
        /// Gets or sets the player's training.
        /// </summary>
        public Training Training { get; set; }

        /// <summary>
        /// Gets or sets the template id.
        /// </summary>
        public int? TemplateId { get; set; }

        /// <summary>
        /// Gets or sets the template.
        /// </summary>
        public ExerciceTemplate Template { get; set; }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        [Required]
        public int Order { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        [Required]
        public ExerciceType Type { get; set; }

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