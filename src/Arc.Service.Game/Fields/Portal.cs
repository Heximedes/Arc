using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arc.Core.Templates.Field;

namespace Arc.Service.Game.Fields
{
    public class Portal : IPortal
    {
        private readonly IField _field;
        private readonly PortalTemplate _template;

        public Portal(IField field, PortalTemplate template)
        {
            _field = field;
            _template = template;
        }
    }
}
