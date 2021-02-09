using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Arc.Core.Templates.Field
{
    public class FieldTemplate : ITemplate
    {
        public FieldTemplate(int id)
        {
            ID = id;
            Portals = new Dictionary<int, PortalTemplate>();
        }

        public int ID { get; set; }

        public Rectangle Bounds { get; set; }   
        public FieldLimitations Limits { get; set; }

        public string ScriptOnFirstUserEnter { get; set; }
        public string ScriptOnUserEnter { get; set; }
        public string ScriptField { get; set; }

        public int ReturnField { get; set; }
        public int ForcedReturnField { get; set; }

        public float MobRate { get; set; }
        public int MobCapacity { get; set; }




        public IDictionary<int, PortalTemplate> Portals { get; }

    }
}
