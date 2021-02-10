using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Arc.Core.Loader.Xml
{
    public class XmlDataNode
    {
        private readonly XElement _xmlElement;

        public XmlDataNode(XElement xmlElement)
        {
            _xmlElement = xmlElement;
        }


        public string Name => _xmlElement.FirstAttribute.Value;


        public XmlDataNode Resolve(string path = null)
            => new XmlDataNode(_xmlElement.Elements().Single(n => n.FirstAttribute.Value == path));
        
        public XmlDataNode ResolveAll()
            => new XmlDataNode(_xmlElement);
        
        public void ResolveAll(Action<XmlDataNode> context)
             => context.Invoke(ResolveAll());
        
        public T? Resolve<T>(string path = null) where T : struct
            => (T?)Convert.ChangeType(_xmlElement.Elements().Single(n => n.FirstAttribute.Value == path).LastAttribute.Value, typeof(T));
    }
}
