using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;

namespace My.CoachManager.Presentation.Prism.ViewModels
{
    /// <summary>
    /// Provides properties for a Country item.
    /// </summary>
    [MetadataType(typeof(CountryMetadata))]
    public class CountryViewModel : DataEntityViewModel
    {
        #region Constants

        public const string FolderPath = "pack://application:,,,/My.CoachManager.Presentation.Prism.Resources;component/Pictures/Countries/";

        #endregion Constants

        private string _flag;

        /// <summary>
        /// Gets or sets the path of the flag image.
        /// </summary>
        public virtual string Flag { get { return _flag; } set { SetProperty(ref _flag, value); } }

        /// <summary>
        /// Get the path of the flag image.
        /// </summary>
        public virtual string FullPath
        {
            get { return FolderPath + Flag; }
        }
    }
}