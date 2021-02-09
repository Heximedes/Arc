using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arc.Database.Models.Inventories.Items
{
    [Table("equips")]
    public class ItemEquip : ItemBase
    {
        [Column("title")]
        public string Title { get; set; }

        [Column(("equipped_date"), TypeName = "DATETIME")]
        public DateTime? EquippedDate { get; set; }

        [Column("str")]
        public int Str { get; set; }

        [Column("dex")]
        public int Dex { get; set; }

        [Column("int")]
        public int Int { get; set; }

        [Column("luk")]
        public int Luk { get; set; }

        [Column("max_hp")]
        public int MaxHP { get; set; }

        [Column("max_mp")]
        public int MaxMP { get; set; }

        [Column("pad")]
        public int Pad { get; set; }

        [Column("mad")]
        public int Mad { get; set; }

        [Column("base_stats_makeup")]
        public long BaseStatsMakeup { get; set; }

        [Column("total_upgrade_count")]
        public byte TotalUpgradeCount { get; set; }

        [Column("current_upgrade_count")]
        public byte CurrentUpgradeCount { get; set; }

        [Column("increased_upgrade_count")]
        public byte IncreasedUpgradeCount { get; set; }

        [Column("current_enhancement_upgrade_count")]
        public byte CurrentEnhancementUpgradeCount { get; set; }

        [Column("potential_grade")]
        public byte PotentialGrade { get; set; }

        [Column("potentials")]
        public string Potentials { get; set; }

        [Column("arcane_force")]
        public short ArcaneForce { get; set; }

        [Column("symbol_experience")]
        public int SymbolExperience { get; set; }

        [Column("symbol_level")]
        public byte SymbolLevel { get; set; }


    }
}
