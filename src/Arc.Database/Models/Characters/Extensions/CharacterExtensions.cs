

using System.Collections.Generic;
using System.Linq;
using Arc.Core.Gameplay.Types;
using Arc.Database.Models.Inventories;
using Arc.Database.Models.Inventories.Items;

namespace Arc.Database.Models.Characters.Extensions
{
    public static class CharacterExtensions
    {
        public static AvatarLook AvatarLook(this Character character)
        {
            return character.AvatarLooks.FirstOrDefault();
        }

        public static AvatarLook ZeroAvatarLook(this Character character)
        {
            return character.AvatarLooks.Single(avatarLook => avatarLook.Gender == Gender.Female);
        }

        public static List<ItemEquip> GetEquippedInventory(this Character character)
        {
            return character.Equips.Where(equip => (equip.BagIndex > (short)BodyPart.BPBase) && (equip.BagIndex <= (short)BodyPart.BPEnd)).ToList();
        }

        public static List<ItemEquip> GetVisibleEquips(this Character character)
        {
            return character.Equips.Where(equip => equip.isEquip()).ToList();
        }

        public static int GetInventorySlotAmount(this Character character, InventoryType type)
        {
            List<string> invSlots = character.InventorySlots.Split("|").ToList();
            switch (type)
            {
                case InventoryType.Equip:
                    return int.Parse(invSlots[0]);
                case InventoryType.Consume:
                    return int.Parse(invSlots[1]);
                case InventoryType.Install:
                    return int.Parse(invSlots[2]);
                case InventoryType.Etc:
                    return int.Parse(invSlots[3]);
                case InventoryType.Cash:
                    return int.Parse(invSlots[4]);
                case InventoryType.Decoration:
                    return int.Parse(invSlots[5]);
                default:
                    return 0;
            }
        }
    }
}
