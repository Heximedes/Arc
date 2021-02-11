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

        public IEnumerable<XmlDataNode> Children => _xmlElement.Elements().Select(c => new XmlDataNode(c));
        public XmlDataNode Resolve(string path = null)
            => new XmlDataNode(_xmlElement.Elements().Single(n => n.FirstAttribute.Value == path));
        
        public XmlDataNode ResolveAll()
            => new XmlDataNode(_xmlElement);
        
        public void ResolveAll(Action<XmlDataNode> context)
             => context.Invoke(ResolveAll());
        
        public T? Resolve<T>(string path = null) where T : struct
        {
            var result = _xmlElement.Elements().SingleOrDefault(n => n.FirstAttribute.Value == path)?.LastAttribute.Value;
            if (result is IConvertible)
            {
                if (typeof(T) == typeof(bool))
                {
                    return result == "0" ? (T)(object)false : (T)(object)true;
                }
                return (T)Convert.ChangeType(result, typeof(T));
            }
            return null;
        }
          

        public string ResolveString(string path = null)
            => _xmlElement.Elements().SingleOrDefault(n => n.FirstAttribute.Value == path)?.LastAttribute.Value;
    }
}
