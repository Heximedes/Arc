using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arc.Database.Models.Inventories.Items
{
    [Table("items")]
    public class ItemStackable : ItemBase
    {
        [Column("quantity")]
        public short Quantity { get; set; }
        [Column("owner")]
        public string Owner { get; set; }
        [Column("attribute")]
        public short Attribute { get; set; }

    }
}
