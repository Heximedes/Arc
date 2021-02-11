using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.Linq;
using Arc.Core.Loader.Xml;
using Arc.Core.Templates.Field.Life;

namespace Arc.Core.Templates.Field
{
    public class FieldTemplate : ITemplate
    {
        public FieldTemplate(int id, XmlDataNode fieldNode)
        {
            ID = id;

            Footholds = fieldNode.Resolve("foothold").Children.SelectMany(c => c.Children).SelectMany(c => c.Children)
                .Select(f => new FootholdTemplate(f)).ToImmutableDictionary(x => x.ID, x => x);

            Portals = fieldNode.Resolve("portal").Children
                .Select(p => new PortalTemplate(p)).ToImmutableDictionary(x => x.ID, x => x);

            Lifes = fieldNode.Resolve("life").Children
                .Select(p => new LifeTemplate(p.ResolveAll()))
                .ToImmutableList();

            fieldNode.Resolve("info").ResolveAll(i =>
            {
                Limits = (FieldLimitations)(i.Resolve<int>("fieldLimit"));


                ReturnField = i.Resolve<int>("returnMap");
                ForcedReturnField = i.Resolve<int>("forcedReturn");

                ScriptOnFirstUserEnter = i.ResolveString("onFirstUserEnter");
                ScriptOnUserEnter = i.ResolveString("onUserEnter");

                MobRate = i.Resolve<float>("mobRate") ?? 1.0f;

                var footholds = Footholds.Values;
                var leftTop = new Point(
                    footholds.Select(f => f.X1 > f.X2 ? f.X2 : f.X1).OrderBy(f => f).First(),
                    footholds.Select(f => f.Y1 > f.Y2 ? f.Y2 : f.Y1).OrderBy(f => f).First()
                );
                var rightBottom = new Point(
                    footholds.Select(f => f.X1 > f.X2 ? f.X1 : f.X2).OrderByDescending(f => f).First(),
                    footholds.Select(f => f.Y1 > f.Y2 ? f.Y1 : f.Y2).OrderByDescending(f => f).First()
                );

                leftTop = new Point(
                    i.Resolve<int>("VRLeft") ?? leftTop.X,
                    i.Resolve<int>("VRTop") ?? leftTop.Y
                );
                rightBottom = new Point(
                    i.Resolve<int>("VRRight") ?? rightBottom.X,
                    i.Resolve<int>("VRBottom") ?? rightBottom.Y
                );
                Bounds = Rectangle.FromLTRB(leftTop.X, leftTop.Y, rightBottom.X, rightBottom.Y);
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



        public IDictionary<int, FootholdTemplate> Footholds { get; }
        public IDictionary<int, PortalTemplate> Portals { get; }
        public ICollection<LifeTemplate> Lifes { get; }



    }
}
