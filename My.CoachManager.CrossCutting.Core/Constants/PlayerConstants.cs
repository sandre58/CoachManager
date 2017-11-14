using My.CoachManager.CrossCutting.Core.Enums;

namespace My.CoachManager.CrossCutting.Core.Constants
{
    /// <summary>
    /// The list of permissions.
    /// </summary>
    public static class PlayerConstants
    {
        public const GenderType DefaultGender = GenderType.Male;

        public const Laterality DefaultLaterality = Laterality.RightHander;

        public const LicenseState DefaultLicenseState = LicenseState.Unknown;

        public const bool DefaultMutation = false;

        public static readonly string[] DefaultSizes = { "XXS", "XS", "M", "L", "XL", "XXL" };

        public const int MinNumber = 1;

        public const int MaxNumber = 99;
    }
}