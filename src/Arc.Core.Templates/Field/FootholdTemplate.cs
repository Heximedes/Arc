using System;
using System.Collections.Generic;
using System.Text;

namespace Arc.Core.Templates.Field
{
    public class FootholdTemplate : ITemplate
    {
        public int ID { get; set; }
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }
        public int Next { get; set; }
        public int Previous { get; set; }

        public FootholdTemplate()
        {

        }
    }
}
