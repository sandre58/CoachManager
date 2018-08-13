using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.Modules.Administration.Enums;

namespace My.CoachManager.Presentation.Prism.Modules.Administration.ViewModels
{
    public class PlayersListParametersViewModel : ListParametersViewModel
    {
        #region Constants

        private static readonly string[] GeneralInformationsColumns =
            {"Age", "Birthdate", "Category", "Gender", "Country", "Address", "Phone", "Email"};

        private static readonly string[] ClubInformationsColumns = { "Number", "Category", "License", "LicenseState" };

        private static readonly string[] BodyInformationsColumns =
            {"Laterality", "Height", "Weight", "Size", "ShoesSize"};

        #endregion Constants

        #region Initialisation

        /// <summary>
        /// Initialise a new instance of <see cref="ListParametersViewModel"/>.
        /// </summary>
        public PlayersListParametersViewModel()
        {
            AddPresetColumns(PresetColumnsType.GeneralInformations, GeneralInformationsColumns);
            AddPresetColumns(PresetColumnsType.ClubInformations, ClubInformationsColumns);
            AddPresetColumns(PresetColumnsType.BodyInformations, BodyInformationsColumns);
        }

        #endregion Initialisation
    }
}