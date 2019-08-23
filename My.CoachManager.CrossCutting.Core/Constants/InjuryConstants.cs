using System;

using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.CrossCutting.Resources.Entities;

namespace My.CoachManager.CrossCutting.Core.Constants
{
    public static class InjuryConstants
    {
        public static string GetDefaultCondition(InjuryType type)
        {
            switch (type)
            {
                case InjuryType.Other:
                    return string.Empty;
                case InjuryType.Head:
                    return InjuryResources.DefaultInjuryHead;
                case InjuryType.Neck:
                    return InjuryResources.DefaultInjuryNeck;
                case InjuryType.Torso:
                    return InjuryResources.DefaultInjuryTorso;
                case InjuryType.Back:
                    return InjuryResources.DefaultInjuryBack;
                case InjuryType.LeftElbow:
                    return InjuryResources.DefaultInjuryLeftElbow;
                case InjuryType.RightElbow:
                    return InjuryResources.DefaultInjuryRightElbow;
                case InjuryType.LeftWrist:
                    return InjuryResources.DefaultInjuryLeftWrist;
                case InjuryType.RightWrist:
                    return InjuryResources.DefaultInjuryRightWrist;
                case InjuryType.LeftShoulder:
                    return InjuryResources.DefaultInjuryLeftShoulder;
                case InjuryType.RightShoulder:
                    return InjuryResources.DefaultInjuryRightShoulder;
                case InjuryType.LeftHand:
                    return InjuryResources.DefaultInjuryLeftHand;
                case InjuryType.RightHand:
                    return InjuryResources.DefaultInjuryRightHand;
                case InjuryType.LeftThigh:
                    return InjuryResources.DefaultInjuryLeftThigh;
                case InjuryType.RightThigh:
                    return InjuryResources.DefaultInjuryRightThigh;
                case InjuryType.LeftKnee:
                    return InjuryResources.DefaultInjuryLeftKnee;
                case InjuryType.RightKnee:
                    return InjuryResources.DefaultInjuryRightKnee;
                case InjuryType.LeftAnkle:
                    return InjuryResources.DefaultInjuryLeftAnkle;
                case InjuryType.RightAnkle:
                    return InjuryResources.DefaultInjuryRightAnkle;
                case InjuryType.LeftFoot:
                    return InjuryResources.DefaultInjuryLeftFoot;
                case InjuryType.RightFoot:
                    return InjuryResources.DefaultInjuryRightFoot;
                case InjuryType.LeftShin:
                    return InjuryResources.DefaultInjuryLeftShin;
                case InjuryType.RightShin:
                    return InjuryResources.DefaultInjuryRightShin;
                case InjuryType.Stomach:
                    return InjuryResources.DefaultInjuryStomach;
                case InjuryType.LeftArm:
                    return InjuryResources.DefaultInjuryLeftArm;
                case InjuryType.RightArm:
                    return InjuryResources.DefaultInjuryRightArm;
                case InjuryType.LeftCalf:
                    return InjuryResources.DefaultInjuryLeftCalf;
                case InjuryType.RightCalf:
                    return InjuryResources.DefaultInjuryRightCalf;
                case InjuryType.Adductors:
                    return InjuryResources.DefaultInjuryAdductors;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
