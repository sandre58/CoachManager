using System.ComponentModel.DataAnnotations;

using My.CoachManager.CrossCutting.Resources.Entities;
using My.CoachManager.Presentation.Core.Models;

namespace My.CoachManager.Presentation.Models
{
    /// <summary>
    /// Provides properties for a Country item.
    /// </summary>
    public class CountryModel : ReferenceModel
    {
        #region Constants

        private const string FolderPath = "pack://application:,,,/My.CoachManager.Presentation.Wpf.Resources;component/Pictures/Countries/";

        #endregion Constants

        /// <summary>
        /// Gets or sets the path of the flag image.
        /// </summary>
        [Display(Name = "Flag", ResourceType = typeof(CountryResources))]
        public virtual string Flag { get; set; }

        /// <summary>
        /// Get the path of the flag image.
        /// </summary>
        public virtual string FullPath => FolderPath + Flag;

    }
}
