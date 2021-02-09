using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arc.Core.Gameplay.Constants;
using Arc.Core.Types;
using Arc.Database.Models.Inventories.Items;
using Arc.Network.Packets;

namespace Arc.Core.Extensions
{
    public static class ItemEncodings
    {
        public static void Encode(this ItemBase item, IPacket outPacket)
        {
            outPacket.Encode<byte>(1); //Type 1 - stack, 2 - equip, 3 - pet
            outPacket.Encode<int>(item.TemplateID);
            outPacket.Encode<bool>(item.CashItemSN.HasValue);
            if (item.CashItemSN.HasValue)
            {
                outPacket.Encode<long>(item.CashItemSN.Value);
            }


            outPacket.Encode<DateTime>(item.ExpiryDate ?? ItemConstants.Permanent); // Move constant
            outPacket.Encode<int>(item.BagIndex);
            outPacket.Encode<byte>(0);
            switch (item)
            {
                case ItemEquip equip:
                    equip.Encode(outPacket);
                    break;
                case ItemStackable stack:
                    stack.Encode(outPacket);
                    break;
                case ItemPet pet:
                    pet.Encode(outPacket);
                    break;
            }
        }


        private static void Encode(this ItemEquip equip, IPacket outPacket)
        {
            BaseEquipStats baseMask = 0;
            outPacket.Encode<int>((int)baseMask);
            if (baseMask.HasFlag(BaseEquipStats.TotalUpgradeCount))
            {

            }
            if (baseMask.HasFlag(BaseEquipStats.CurrentUpgradeCount))
            {

            }
            if (baseMask.HasFlag(BaseEquipStats.StrIncrease))
            {

            }
            if (baseMask.HasFlag(BaseEquipStats.DexIncrease))
            {

            }
            if (baseMask.HasFlag(BaseEquipStats.IntIncrease))
            {

            }
            if (baseMask.HasFlag(BaseEquipStats.LukIncrease))
            {

            }
            if (baseMask.HasFlag(BaseEquipStats.MaxHPIncrease))
            {

            }
            if (baseMask.HasFlag(BaseEquipStats.MaxMPIncrease))
            {

            }
            if (baseMask.HasFlag(BaseEquipStats.TotalUpgradeCount))
            {

            }
            if (baseMask.HasFlag(BaseEquipStats.TotalUpgradeCount))
            {

            }
            if (baseMask.HasFlag(BaseEquipStats.TotalUpgradeCount))
            {

            }
            if (baseMask.HasFlag(BaseEquipStats.TotalUpgradeCount))
            {

            }
            if (baseMask.HasFlag(BaseEquipStats.TotalUpgradeCount))
            {

            }
            if (baseMask.HasFlag(BaseEquipStats.TotalUpgradeCount))
            {

            }
            if (baseMask.HasFlag(BaseEquipStats.TotalUpgradeCount))
            {

            }
            if (baseMask.HasFlag(BaseEquipStats.TotalUpgradeCount))
            {

            }
            if (baseMask.HasFlag(BaseEquipStats.TotalUpgradeCount))
            {

            }
            if (baseMask.HasFlag(BaseEquipStats.TotalUpgradeCount))
            {

            }
            if (baseMask.HasFlag(BaseEquipStats.TotalUpgradeCount))
            {

            }
            if (baseMask.HasFlag(BaseEquipStats.TotalUpgradeCount))
            {

            }

            ExtendedEquipStats extendedMask = 0;
            outPacket.Encode<int>((int)extendedMask);
            if (extendedMask.HasFlag(ExtendedEquipStats.AllStatsPercentIncrease))
            {

            }

            if (extendedMask.HasFlag(ExtendedEquipStats.AllStatsPercentIncrease))
            {

            }

            if (extendedMask.HasFlag(ExtendedEquipStats.AllStatsPercentIncrease))
            {

            }

            if (extendedMask.HasFlag(ExtendedEquipStats.AllStatsPercentIncrease))
            {

            }

            if (extendedMask.HasFlag(ExtendedEquipStats.AllStatsPercentIncrease))
            {

            }

            outPacket.Encode<string>(equip.Title ?? "");
            outPacket.Encode<byte>(equip.PotentialGrade);
            outPacket.Encode<byte>(equip.CurrentEnhancementUpgradeCount);

            for (int i = 0; i < 7; i++)
            {
                outPacket.Encode<short>(0);
            }

            outPacket.Encode<short>(0);
            for (int i = 0; i < 3; i++)
            {
                outPacket.Encode<short>(0);
            }

            outPacket.Encode<int>(0);

            // is not cash >> encode sn
            if (!equip.CashItemSN.HasValue)
            {
                outPacket.Encode<long>(1);
            }

            outPacket.Encode<DateTime>(ItemConstants.Permanent);

            outPacket.Encode<long>(0);
            outPacket.Encode<DateTime>(equip.ExpiryDate ?? ItemConstants.Permanent);
            outPacket.Encode<int>(0);// grade
            for (int i = 0; i < 3; i++)
            {
                outPacket.Encode<int>(0);
            }
            // end GW_CashItemOption
            outPacket.Encode<short>(0); // soul ID
            outPacket.Encode<short>(0); // enchanter ID
            outPacket.Encode<short>(0); // optionID (same as potentials)
            if (equip.TemplateID / 10000 == 171)
            { // Arcane Symbol
                outPacket.Encode<short>(0);// Arcane
                outPacket.Encode<int>(0);// Arcane EXP
                outPacket.Encode<short>(0);// Arcane Max Level
            }
            outPacket.Encode<short>(0);
            outPacket.Encode<long>(0);
            outPacket.Encode<long>(0);
            outPacket.Encode<long>(0);
            if (equip.TemplateID / 10000 == 166)
            { // Android
                outPacket.Encode<short>(0);
                outPacket.Encode<short>(0);
                outPacket.Encode<short>(0);
                outPacket.Encode<string>("");
                outPacket.Encode<string>("");
                outPacket.Encode<long>(0);
            }

        }

        private static void Encode(this ItemStackable stack, IPacket outPacket)
        {
            outPacket.Encode<short>(stack.Quantity);
            outPacket.Encode<string>(stack.Owner);
            outPacket.Encode<short>(stack.Attribute); // some flag
            //if throwing stars / bullets / familiar / intense power crystals
            // encode some long mask
            outPacket.Encode<int>(0);

        }

        private static void Encode(this ItemPet pet, IPacket outPacket)
        {

        }
    }
}
