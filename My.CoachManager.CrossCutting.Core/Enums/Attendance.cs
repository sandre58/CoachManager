using System.ComponentModel.DataAnnotations;

using My.CoachManager.CrossCutting.Core.Resources.Enums;

namespace My.CoachManager.CrossCutting.Core.Enums
{
    public enum Attendance
    {
        [Display(Name = "Unknown", ResourceType = typeof(AttendanceResources))]
        Unknown,

        [Display(Name = "Present", ResourceType = typeof(AttendanceResources))]
        Present,

        [Display(Name = "Absent", ResourceType = typeof(AttendanceResources))]
        Absent,

        [Display(Name = "Apology", ResourceType = typeof(AttendanceResources))]
        Apology,

        [Display(Name = "Injured", ResourceType = typeof(AttendanceResources))]
        Injured,

        [Display(Name = "InSelection", ResourceType = typeof(AttendanceResources))]
        InSelection,

        [Display(Name = "Resting", ResourceType = typeof(AttendanceResources))]
        Resting,

    }
}
