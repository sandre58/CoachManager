﻿namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Entities
{
    public interface IEntityViewModel : IViewModel
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        int Id { get; set; }
    }
}