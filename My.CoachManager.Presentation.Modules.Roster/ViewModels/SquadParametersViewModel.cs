using My.CoachManager.Presentation.Core.ViewModels;
using My.CoachManager.Presentation.Modules.Shared.Enums;

namespace My.CoachManager.Presentation.Modules.Roster.ViewModels
{
    public class SquadParametersViewModel : ListParameters
    {
        #region Constants

        private static readonly string[] GeneralInformationsColumns =
            {"Age", "Birthdate", "Category", "Gender", "Country", "Address", "Phone", "Email"};

        private static readonly string[] ClubInformationsColumns = { "Number", "Category", "Position", "LicenseNumber", "LicenseState", "FromDate" };

        private static readonly string[] BodyInformationsColumns =
            {"Laterality", "Height", "Weight", "Size", "ShoesSize"};

        #endregion Constants

        #region Initialisation

        /// <summary>
        /// Initialise a new instance of <see cref="SquadParametersViewModel"/>.
        /// </summary>
        public SquadParametersViewModel()
        {
            AddPresetColumns(PresetPlayerColumnsType.GeneralInformations, GeneralInformationsColumns);
            AddPresetColumns(PresetPlayerColumnsType.ClubInformations, ClubInformationsColumns);
            AddPresetColumns(PresetPlayerColumnsType.BodyInformations, BodyInformationsColumns);

            ChangeDisplayedColumns(PresetPlayerColumnsType.ClubInformations);
        }

        #endregion Initialisation
    }
}