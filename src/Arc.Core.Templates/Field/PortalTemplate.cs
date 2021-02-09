using System;
using System.Collections.Generic;
using System.Text;
using Arc.Core.Templates.Field.Types;

namespace Arc.Core.Templates.Field
{
    public class PortalTemplate : ITemplate
    {
        public int ID { get; }
        public PortalType Type { get; set; }

        public string Name { get; set; }
        public string Script { get; set; }

        public int ToMap { get; set; }

        public PortalTemplate(int id)
        {
        }
    }
}
