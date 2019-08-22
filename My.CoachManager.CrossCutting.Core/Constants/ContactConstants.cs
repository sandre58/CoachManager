using My.CoachManager.CrossCutting.Core.Resources.Entities;

namespace My.CoachManager.CrossCutting.Core.Constants
{
    /// <summary>
    /// The list of permissions.
    /// </summary>
    public static class ContactConstants
    {
        public const string PhoneRegex = "^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$";
        public static readonly string[] DefaultPhoneLabels = { ContactResources.Mobile, ContactResources.Home, ContactResources.Professional, ContactResources.Father, ContactResources.Mother, ContactResources.Fax, ContactResources.Other };
        public static readonly string[] DefaultEmailLabels = { ContactResources.Home, ContactResources.Professional, ContactResources.Father, ContactResources.Mother, ContactResources.Other };
    }
}
