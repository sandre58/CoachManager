using My.CoachManager.Presentation.Core.ViewModels;
using My.CoachManager.Presentation.Modules.Administration.Enums;

namespace My.CoachManager.Presentation.Modules.Administration.ViewModels
{
    public class PlayersListParametersViewModel : ListParameters
    {
        #region Constants

        private static readonly string[] GeneralInformationsColumns =
            {"Age", "Birthdate", "Gender", "Country", "Address", "Phone", "Email"};

        private static readonly string[] ClubInformationsColumns = { "Position", "LicenseNumber", "FromDate" };

        private static readonly string[] BodyInformationsColumns =
            {"Laterality", "Height", "Weight", "Size", "ShoesSize"};

        #endregion Constants

        #region Initialisation

        /// <summary>
        /// Initialise a new instance of <see cref="ListParameters"/>.
        /// </summary>
        public PlayersListParametersViewModel()
        {
            AddPresetColumns(PresetPlayerColumnsType.GeneralInformations, GeneralInformationsColumns);
            AddPresetColumns(PresetPlayerColumnsType.ClubInformations, ClubInformationsColumns);
            AddPresetColumns(PresetPlayerColumnsType.BodyInformations, BodyInformationsColumns);

            ChangeDisplayedColumns(PresetPlayerColumnsType.GeneralInformations);
        }

        #endregion Initialisation
    }
}