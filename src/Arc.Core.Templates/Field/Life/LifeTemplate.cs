using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Arc.Core.Loader.Xml;
using Arc.Core.Templates.Field.Life.Types;


namespace Arc.Core.Templates.Field.Life
{
    public class LifeTemplate : ITemplate
    {
        public LifeTemplate(XmlDataNode lifeNode)
        {
            ID = lifeNode.Resolve<int>("id") ?? -1;
            Type = lifeNode.ResolveString("type").ToLower() == "n"
                ? LifeType.Npc
                : LifeType.Monster;
            MobTime = lifeNode.Resolve<int>("mobTime") ?? 0;
            Flipped = !(lifeNode.Resolve<bool>("f") ?? false);
            Hidden = !(lifeNode.Resolve<bool>("hide") ?? false);
            ShortenedName = lifeNode.ResolveString("limitedname");
            Position = new Point(
                lifeNode.Resolve<int>("x") ?? int.MinValue,
                lifeNode.Resolve<int>("y") ?? int.MinValue
            );
            RX0 = lifeNode.Resolve<int>("rx0") ?? int.MinValue;
            RX1 = lifeNode.Resolve<int>("rx1") ?? int.MinValue;
            Foothold = lifeNode.Resolve<int>("fh") ?? 0;
        }

        public int ID { get; }
        public LifeType Type { get; }

        public Point Position { get; }
        public bool Flipped { get; }
        public bool Hidden { get; }
        public int Foothold { get; }
        public string ShortenedName { get; }

        public int RX0 { get; }
        public int RX1 { get; }

        public int MobTime { get; }

    }
}
