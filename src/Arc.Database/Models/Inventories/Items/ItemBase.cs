using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arc.Database.Models.Inventories.Items
{
    public abstract class ItemBase
    {
        [Column("id")]
        public int ID { get; set; }

        [Column("template_id")]
        public int TemplateID { get; set; }

        [Column("cash_item_sn")]
        public long? CashItemSN { get; set; }

        [Column("expiry_date")]
        public DateTime? ExpiryDate { get; set; }

        [Column("bag_index")]
        public int BagIndex { get; set; }
    }
}
