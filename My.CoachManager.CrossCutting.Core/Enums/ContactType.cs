using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Resources.Enums;

namespace My.CoachManager.CrossCutting.Core.Enums
{
    /// <summary>
    /// Contacts Types.
    /// </summary>
    public enum ContactType
    {
        [Display(Name = "Phone", ResourceType = typeof(ContactTypeResources))]
        Phone = 1,

        [Display(Name = "Email", ResourceType = typeof(ContactTypeResources))]
        Email = 2
    }
}