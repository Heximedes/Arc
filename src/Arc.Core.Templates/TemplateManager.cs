using System;
using System.Collections.Generic;
using System.Text;

namespace Arc.Core.Templates
{
    public class TemplateManager : ITemplateManager
    {
        public T Get<T>(int id) where T : ITemplate
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll<T>() where T : ITemplate
        {
            throw new NotImplementedException();
        }
    }
}
