using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arc.Core.Distributed.Nodes;
using Arc.Core.Templates.Field;

namespace Arc.Service.Game.Fields
{
    public interface IField : IServiceTick
    {
        public int ID { get; }

        public FieldTemplate Template { get; }
    }
}
