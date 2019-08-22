using My.CoachManager.Presentation.Wpf.Core.ViewModels;
using My.CoachManager.Presentation.Wpf.Modules.Admin.Enums;

namespace My.CoachManager.Presentation.Wpf.Modules.Admin.ViewModels
{
    public class PlayersListParametersViewModel : ListParameters
    {
        #region Constants

        private static readonly string[] GeneralInformationColumns =
            {"Age", "Birthdate", "Gender", "Country", "Address", "Phone", "Email"};

        private static readonly string[] ClubInformationColumns = { "NaturalPosition", "LicenseNumber", "FromDate" };

        private static readonly string[] BodyInformationColumns =
            {"Laterality", "Height", "Weight", "Size", "ShoesSize"};

        #endregion Constants

        #region Initialisation

        /// <summary>
        /// Initialise a new instance of <see cref="PlayersListParametersViewModel"/>.
        /// </summary>
        public PlayersListParametersViewModel()
        {
            AddPresetColumns(PresetPlayerColumnsType.GeneralInformation, GeneralInformationColumns);
            AddPresetColumns(PresetPlayerColumnsType.ClubInformation, ClubInformationColumns);
            AddPresetColumns(PresetPlayerColumnsType.BodyInformation, BodyInformationColumns);

            ChangeDisplayedColumns(PresetPlayerColumnsType.GeneralInformation);
        }

        #endregion Initialisation
    }
}
