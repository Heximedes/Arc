using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arc.Database.Models.Inventories.Items
{
    public static class ItemEquipExtensions
    {
        public static bool isEquip(this ItemEquip item)
        {
            return item.TemplateID / 1000000 == 1;
        }
    }
}
