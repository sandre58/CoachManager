using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Resources.Enums;

namespace My.CoachManager.CrossCutting.Core.Enums
{
    public enum InjuryType
    {
        [Display(Name = "Other", ResourceType = typeof(InjuryTypeResources))]
        Other,

        [Display(Name = "Head", ResourceType = typeof(InjuryTypeResources))]
        Head,

        [Display(Name = "Neck", ResourceType = typeof(InjuryTypeResources))]
        Neck,

        [Display(Name = "Torso", ResourceType = typeof(InjuryTypeResources))]
        Torso,

        [Display(Name = "LeftElbow", ResourceType = typeof(InjuryTypeResources))]
        LeftElbow,

        [Display(Name = "RightElbow", ResourceType = typeof(InjuryTypeResources))]
        RightElbow,

        [Display(Name = "LeftWrist", ResourceType = typeof(InjuryTypeResources))]
        LeftWrist,

        [Display(Name = "RightWrist", ResourceType = typeof(InjuryTypeResources))]
        RightWrist,

        [Display(Name = "LeftShoulder", ResourceType = typeof(InjuryTypeResources))]
        LeftShoulder,

        [Display(Name = "RightShoulder", ResourceType = typeof(InjuryTypeResources))]
        RightShoulder,

        [Display(Name = "LeftHand", ResourceType = typeof(InjuryTypeResources))]
        LeftHand,

        [Display(Name = "RightHand", ResourceType = typeof(InjuryTypeResources))]
        RightHand,

        [Display(Name = "LeftThigh", ResourceType = typeof(InjuryTypeResources))]
        LeftThigh,

        [Display(Name = "RightThigh", ResourceType = typeof(InjuryTypeResources))]
        RightThigh,

        [Display(Name = "LeftKnee", ResourceType = typeof(InjuryTypeResources))]
        LeftKnee,

        [Display(Name = "RightKnee", ResourceType = typeof(InjuryTypeResources))]
        RightKnee,

        [Display(Name = "LeftAnkle", ResourceType = typeof(InjuryTypeResources))]
        LeftAnkle,

        [Display(Name = "RightAnkle", ResourceType = typeof(InjuryTypeResources))]
        RightAnkle,

        [Display(Name = "LeftFoot", ResourceType = typeof(InjuryTypeResources))]
        LeftFoot,

        [Display(Name = "RightFoot", ResourceType = typeof(InjuryTypeResources))]
        RightFoot,

        [Display(Name = "LeftShin", ResourceType = typeof(InjuryTypeResources))]
        LeftShin,

        [Display(Name = "RightShin", ResourceType = typeof(InjuryTypeResources))]
        RightShin,

        [Display(Name = "Stomach", ResourceType = typeof(InjuryTypeResources))]
        Stomach,

        [Display(Name = "LeftArm", ResourceType = typeof(InjuryTypeResources))]
        LeftArm,

        [Display(Name = "RightArm", ResourceType = typeof(InjuryTypeResources))]
        RightArm
    }
}