using System;
using System.Drawing;
using Arc.Core.Loader.Xml;
using Arc.Core.Templates.Field.Types;

namespace Arc.Core.Templates.Field
{
    public class PortalTemplate : ITemplate
    {
        public int ID { get; }
        public PortalType Type { get; }

        public string Name { get; }
        public string Script { get; }
        public Point Position { get; }

        public int TargetMap { get; }
        public string TargetPortal { get; }

        public PortalTemplate(XmlDataNode portalNode)
        {
            ID = Convert.ToInt32(portalNode.Name);
            Name = portalNode.ResolveString("pn");
            Type = (PortalType)(portalNode.Resolve<int>("pt") ?? 0);
            Script = portalNode.ResolveString("script");
            TargetMap = portalNode.Resolve<int>("tm") ?? int.MinValue;
            TargetPortal = portalNode.ResolveString("tn");

            Position = new Point(
                portalNode.Resolve<int>("x") ?? int.MinValue,
                portalNode.Resolve<int>("y") ?? int.MinValue
            );
        }
    }
}
