using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.Modules.Core.Enums;

namespace My.CoachManager.Presentation.Prism.Modules.Roster.ViewModels
{
    public class RosterParametersViewModel : ListParametersViewModel
    {
        #region Constants

        private static readonly string[] GeneralInformationsColumns =
            {"Age", "Birthdate", "Category", "Gender", "Country", "Address", "Phone", "Email"};

        private static readonly string[] ClubInformationsColumns = { "Number", "Category", "LicenseNumber", "LicenseState" };

        private static readonly string[] BodyInformationsColumns =
            {"Laterality", "Height", "Weight", "Size", "ShoesSize"};

        #endregion Constants

        #region Initialisation

        /// <summary>
        /// Initialise a new instance of <see cref="RosterParametersViewModel"/>.
        /// </summary>
        public RosterParametersViewModel()
        {
            AddPresetColumns(PresetPlayerColumnsType.GeneralInformations, GeneralInformationsColumns);
            AddPresetColumns(PresetPlayerColumnsType.ClubInformations, ClubInformationsColumns);
            AddPresetColumns(PresetPlayerColumnsType.BodyInformations, BodyInformationsColumns);

            ChangeDisplayedColumns(PresetPlayerColumnsType.GeneralInformations);
        }

        #endregion Initialisation
    }
}