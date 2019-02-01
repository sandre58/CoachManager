using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.Domain.Core;

namespace My.CoachManager.Domain.Entities
{
    /// <summary>
    /// Provides properties for a training Entity.
    /// </summary>
    public class Exercice : Entity, IOrderable
    {
        private string _goals;
        private string _instructions;
        private string _variables;
        private string _methods;

        /// <summary>
        /// Initalize a new instance of <see cref="Exercice"/>.
        /// </summary>
        public Exercice()
        {
            Goals = new List<string>();
            Instructions = new List<string>();
            Variables = new List<string>();
            Methods = new List<string>();
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
        public int TemplateId { get; set; }

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
        [NotMapped]
        public ICollection<string> Goals
        {
            get => _goals.Split(';');
            set => _goals = string.Join(";", value);
        }

        /// <summary>
        /// Gets or sets the instructions.
        /// </summary>
        [NotMapped]
        public ICollection<string> Instructions
        {
            get => _instructions.Split(';');
            set => _instructions = string.Join(";", value);
        }

        /// <summary>
        /// Gets or sets the variables.
        /// </summary>
        [NotMapped]
        public ICollection<string> Variables
        {
            get => _variables.Split(';');
            set => _variables = string.Join(";", value);
        }

        /// <summary>
        /// Gets or sets the variables.
        /// </summary>
        [NotMapped]
        public ICollection<string> Methods
        {
            get => _methods.Split(';');
            set => _methods = string.Join(";", value);
        }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        public int Duration { get; set; }
    }
}