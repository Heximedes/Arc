using System;
using System.Collections.Generic;
using System.Text;

namespace Arc.Core.Templates.Field
{
    [Flags]
    public enum FieldLimitations
    {
        UnableToJump = 0x1,
        UnableToUseSkill = 0x2,
        UnableToUseSummonItem = 0x4,
        UnableToUseMysticDoor = 0x8,
        UnableToMigrate = 0x10,
        UnableToUsePortalScroll = 0x20,
        UnableToUseTeleportItem = 0x40,
        UnableToOpenMiniGame = 0x80,
        UnableToUseSpecificPortalScroll = 0x100,
        UnableToUseTamingMob = 0x200,
        UnableToConsumeStatChangeItem = 0x400,
        UnableToChangePartyBoss = 0x800,
        NoMonsterCapacityLimit = 0x1000,
        UnableToUseWeddingInvitationItem = 0x2000, 
        UnableToUseCashWeather = 0x4000,
        UnableToUsePet = 0x8000,
        UnableToUseAntiMacroItem = 0x10000,
        UnableToFallDown = 0x20000,
        UnableToSummonNPC = 0x40000,
        NoEXPDecrease = 0x80000,
        NoDamageOnFalling = 0x100000,
        ParcelOpenLimit = 0x200000,
        DropLimit = 0x400000,
        UnableToUseRocketBoost = 0x800000,
        NoItemOptionLimit = 0x1000000,
        NoQuestAlerts = 0x2000000,
        NoAndroid = 0x4000000,
        AutoExpandMinimap = 0x8000000,
        MoveSkillOnly = 0x10000000

    }
}
