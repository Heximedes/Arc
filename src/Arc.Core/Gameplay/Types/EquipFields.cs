using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arc.Core.Types
{
    [Flags]
    public enum BaseEquipStats : uint
    {
        TotalUpgradeCount = 0x1,
        CurrentUpgradeCount = 0x2,
        StrIncrease = 0x4,
        DexIncrease = 0x8,
        IntIncrease = 0x10,
        LukIncrease = 0x20,
        MaxHPIncrease = 0x40,
        MaxMPIncrease = 0x80,
        AttackIncrease = 0x100,
        MagicAttackIncrease = 0x200,
        DefIncrease = 0x400,
        CraftIncrease = 0x4000,
        SpeedIncrease = 0x8000,
        JumpIncrease = 0x10000,
        attribute = 0x20000,
        levelUpType = 0x40000,
        level = 0x80000,
        exp = 0x100000,
        durability = 0x200000,
        UsedHammerSlots = 0x400000,
        PvpDamage = 0x800000,
        ReduceReq = 0x1000000,
        specialAttribute = 0x2000000,
        durabilityMax = 0x4000000,
        IncReq = 0x8000000,
        growthEnchant = 0x10000000,
        psEnchant = 0x20000000,
        bdr = 0x40000000,
        imdr = 0x80000000
    }

    [Flags]
    public enum ExtendedEquipStats
    {
        DamagePercentIncrease = 0x1,
        AllStatsPercentIncrease = 0x2,
        Cuttable = 0x4, // TODO: Pick a clearer name
        FlameStatsComposition = 0x8,
        HyperUpgrade = 0x10 // What is this?
    }
}
