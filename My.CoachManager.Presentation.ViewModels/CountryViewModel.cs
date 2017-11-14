using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;

namespace My.CoachManager.Presentation.ViewModels
{
    [MetadataType(typeof(CountryMetadata))]
    public class CountryViewModel : DataEntityViewModel
    {
        #region Constants

        public const string FolderPath = "pack://application:,,,/My.CoachManager.Presentation;component/Resources/Pictures/Countries/";

        #endregion Constants

        private string _flag;
        public virtual string Flag { get { return _flag; } set { SetEntityProperty(() => _flag = value, value, Flag); } }

        /// <summary>
        /// Get the full name.
        /// </summary>
        public virtual string FullPath
        {
            get { return FolderPath + Flag; }
        }
    }
}