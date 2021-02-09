using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arc.Core.Templates.Field;

namespace Arc.Service.Game.Fields
{
    public class Field : IField
    {
        public int ID => Template.ID;
        public FieldTemplate Template { get; }


        private readonly IDictionary<string, IPortal> _portals;


        public Field(FieldTemplate template)
        {
            Template = template;

            _portals = template.Portals
                .Where(kv => kv.Value.Name != "sp" && kv.Value.Name != "tp")
                .ToDictionary(
                    kv => kv.Value.Name,
                    kv => (IPortal)new Portal(this, kv.Value)
                );

        }


        public IPortal GetPortal(byte portal)
            => _portals[Template.Portals[portal].Name];

        public IPortal GetPortal(string portal)
            => _portals[portal];



        public async Task OnTick(DateTime now)
        {
            await Task.CompletedTask;
        }
    }
}
