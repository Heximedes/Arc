using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Arc.Core.Loader;
using Arc.Core.Loader.Xml;

namespace Arc.Core.Templates.Field
{
    public class FieldTemplate : ITemplate
    {
        public FieldTemplate(int id, XmlDataNode dataNode)
        {
            
            ID = id;
            dataNode.Resolve("info").ResolveAll(i =>
            {
                Limits = (FieldLimitations)(i.Resolve<int>("fieldLimit") ?? 0);


                ReturnField = i.Resolve<int>("returnMap");
                ForcedReturnField = i.Resolve<int>("forcedReturn");
                if (ReturnField == 999999999) ReturnField = null;
                if (ForcedReturnField == 999999999) ForcedReturnField = null;


            });
        }

        public int ID { get; set; }

        public Rectangle Bounds { get; set; }   
        public FieldLimitations Limits { get; set; }

        public string ScriptOnFirstUserEnter { get; set; }
        public string ScriptOnUserEnter { get; set; }
        public string ScriptField { get; set; }

        public int? ReturnField { get; set; }
        public int? ForcedReturnField { get; set; }

        public float MobRate { get; set; }
        public int MobCapacity { get; set; }




        public IDictionary<int, PortalTemplate> Portals { get; }

    }
}
