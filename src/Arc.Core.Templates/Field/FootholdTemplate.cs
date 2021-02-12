using System;
using System.Collections.Generic;
using System.Text;
using Arc.Core.Loader.Xml;

namespace Arc.Core.Templates.Field
{
    public class FootholdTemplate : ITemplate
    {
        public int ID { get; }
        public int X1 { get; }
        public int Y1 { get; }
        public int X2 { get; }
        public int Y2 { get;  }
        public int Next { get; }
        public int Previous { get; }
        public int ForcedMove { get; }

        public FootholdTemplate(XmlDataNode footholdNode)
        {
            ID = Convert.ToInt32(footholdNode.Name);
            Next = footholdNode.Resolve<int>("next") ?? 0;
            Previous = footholdNode.Resolve<int>("prev") ?? 0;
            X1 = footholdNode.Resolve<int>("x1") ?? 0;
            X2 = footholdNode.Resolve<int>("x2") ?? 0;
            Y1 = footholdNode.Resolve<int>("y1") ?? 0;
            Y2 = footholdNode.Resolve<int>("y2") ?? 0;
            ForcedMove = footholdNode.Resolve<int>("force") ?? 0;
        }
    }
}
