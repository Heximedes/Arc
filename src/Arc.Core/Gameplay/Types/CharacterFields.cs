using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arc.Core.Types
{
    [Flags]
    public enum CharacterFields : long
    {
        None = 0,
        Character = 0x1,
        Money = 0x2,
        EquipItems = 0x4,
        ConsumeItems = 0x8,
        InstallItems = 0x10,
        EtcItems = 0x20,
        CashItems = 0x40,
        InventorySize = 0x80,
        SkillRecord = 0x100,
        QuestRecord = 0x200,
        MinigameRecord = 0x400,
        CoupleRecord = 0x800,
        MapTransfer = 0x1000,
        UNK_0x2000 = 0x2000,
        QuestComplete = 0x4000,
        SkillCooldown = 0x8000,
        UNK_0x10000 = 0x10000,
        UNK_0x20000 = 0x20000,
        QuestRecordEx = 0x40000,
        UNK_0x80000 = 0x80000,
        EquipExtension = 0x100000,
        WildHunterInfo = 0x200000,
        FamiliarManagerInfo = 0x400000,
        ItemPotRecord = 0x800000,
        CoreAura = 0x1000000,
        ExpConsumeItems = 0x2000000,
        ShopBuyLimit = 0x4000000,
        UNK_0x8000000 = 0x8000000,
        ChosenSkills = 0x10000000,
        StolenSkills = 0x20000000,
        UNK_0x40000000 = 0x40000000,
        CharacterPotential = 0x80000000,
        UNK_0x100000000 = 0x100000000,
        UNK_0x200000000 = 0x200000000,
        ReturnEffectInfo = 0x400000000,
        DressUpInfo = 0x800000000,
        CoreInfo = 0x1000000000,
        FarmPotential = 0x2000000000,
        FarmUserInfo = 0x4000000000,
        MemorialCubeInfo = 0x8000000000,
        UNK_0x10000000000 = 0x10000000000,
        ActiveDamageSkin = 0x20000000000,
        LikePointRecord = 0x40000000000,
        ZeroInfo = 0x80000000000,
        UNK_0x100000000000 = 0x100000000000,
        UNK_0x200000000000 = 0x200000000000,
        UNK_0x400000000000 = 0x400000000000,
        RedLeafInfo = 0x800000000000,
        SoulCollection = 0x1000000000000,
        RunnerGameRecord = 0x2000000000000,
        MonsterCollectionRecord = 0x4000000000000,
        UNK_0x8000000000000 = 0x8000000000000,
        UNK_0x10000000000000 = 0x10000000000000,
        VMatrixRecord = 0x20000000000000,
        AchievementRecord = 0x40000000000000,
        UNK_0x80000000000000 = 0x80000000000000,
        MemorialFlameInfo = 0x100000000000000,
        UNK_0x200000000000000 = 0x200000000000000,
        UNK_0x400000000000000 = 0x400000000000000,
        EmoteRecord = 0x800000000000000,
        FamiliarCollectionRecord =  0x1000000000000000,
        UNK_0x2000000000000000 = 0x2000000000000000,
        UNK_0x4000000000000000 = 0x4000000000000000,


        All = -1
    }
}
