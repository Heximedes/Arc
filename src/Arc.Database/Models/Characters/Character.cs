using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arc.Core.Gameplay.Constants.Character.Inventory;
using Arc.Database.Models.Inventories;
using Arc.Database.Models.Inventories.Items;

namespace Arc.Database.Models.Characters
{
    [Table("characters")]
    public class Character : IModel
    {
        [Column("id")]
        public int ID { get; set; }

        [Column("name")]
        [MaxLength(13)]
        public string Name { get; set; }

        [Column("level")]
        public int Level { get; set; }

        [Column("job")]
        public short Job { get; set; }

        [Column("sub_job")]
        public short SubJob { get; set; }


        [Column("str")]
        public short Str { get; set; }

        [Column("dex")]
        public short Dex { get; set; }

        [Column("int")]
        public short Int { get; set; }

        [Column("luk")]
        public short Luk { get; set; }

        [Column("hp")]
        public int HP { get; set; }

        [Column("max_hp")]
        public int MaxHP { get; set; }

        [Column("mp")]
        public int MP { get; set; }

        [Column("max_mp")]
        public int MaxMP { get; set; }


        [Column("ap")]
        public short AP { get; set; }

        [Column("sp")]
        public short SP { get; set; }


        [Column("exp")]
        public int Exp { get; set; }

        [Column("fame")]
        public int Fame { get; set; }

        [Column("money")]
        public long Money { get; set; }


        [Column("current_field")]
        public int CurrentField { get; set; }

        [Column("previous_field")]
        public int PreviousField { get; set; }

        [Column("current_portal")]
        public byte CurrentPortal { get; set; }

        [Column("inventory_slots")]
        public string InventorySlots { get; set; }

        [ForeignKey("char_id")]
        public List<AvatarLook> AvatarLooks { get; set; }

        [ForeignKey("char_id")]
        public List<ItemStackable> Items { get; set; }

        [ForeignKey("char_id")]
        public List<ItemEquip> Equips { get; set; }

        public Character()
        {

        }

        public Character(int[] startingItems)
        {
            Level = 1;
            Str = 12;
            Dex = 5;
            Int = 4;
            Luk = 4;
            HP = 50;
            MaxHP = 50;
            MP = 50;
            MaxMP = 50;

            CurrentField = 100000000;

            InventorySlots = "52|52|52|52|128|128";



            AvatarLooks = new List<AvatarLook>();
            Equips = new List<ItemEquip>();
        }

    }
}
