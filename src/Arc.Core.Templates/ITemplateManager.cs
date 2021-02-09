using System;
using System.Collections.Generic;
using System.Text;

namespace Arc.Core.Templates
{
    public interface ITemplateManager
    {
        T Get<T>(int id) where T : ITemplate;
        IEnumerable<T> GetAll<T>() where T : ITemplate;
    }
}
