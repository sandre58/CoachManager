using My.CoachManager.Presentation.Wpf.Core.ViewModels;
using My.CoachManager.Presentation.Wpf.Modules.Roster.Enums;

namespace My.CoachManager.Presentation.Wpf.Modules.Roster.ViewModels
{
    public class SquadParametersViewModel : ListParameters
    {
        #region Constants

        private static readonly string[] GeneralInformationColumns =
            {"Age", "Birthdate", "Category", "Gender", "Country", "Address", "Phone", "Email"};

        private static readonly string[] ClubInformationColumns = { "Number", "Category", "NaturalPosition", "LicenseNumber", "LicenseState", "FromDate" };

        private static readonly string[] BodyInformationColumns =
            {"Laterality", "Height", "Weight", "Size", "ShoesSize"};

        #endregion Constants

        #region Initialisation

        /// <summary>
        /// Initialise a new instance of <see cref="SquadParametersViewModel"/>.
        /// </summary>
        public SquadParametersViewModel()
        {
            AddPresetColumns(PresetRosterPlayersColumnsType.GeneralInformation, GeneralInformationColumns);
            AddPresetColumns(PresetRosterPlayersColumnsType.ClubInformation, ClubInformationColumns);
            AddPresetColumns(PresetRosterPlayersColumnsType.BodyInformation, BodyInformationColumns);

            ChangeDisplayedColumns(PresetRosterPlayersColumnsType.ClubInformation);
        }

        #endregion Initialisation
    }
}
