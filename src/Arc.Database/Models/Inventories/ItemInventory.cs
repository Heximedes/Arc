using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arc.Database.Models.Inventories.Items;

namespace Arc.Database.Models.Inventories
{
    public class ItemInventory
    {
        public int Slots { get; set; }
        public IDictionary<short, ItemBase> Items { get; set; }
        public ItemBase this[in short slot] => Items[slot];

        public ItemInventory(int slots)
        {
            Slots = slots;
            Items = new Dictionary<short, ItemBase>();
        }
    }
}
