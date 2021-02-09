using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arc.Core.Distributed.Nodes;
using Arc.Core.Templates;
using Arc.Core.Templates.Field;

namespace Arc.Service.Game.Fields
{
    public class FieldHandler : IServiceTick
    {
        private readonly IDictionary<int, IField> _fields;
        private readonly ITemplateManager _templateManager;

        public FieldHandler(ITemplateManager templateManager)
        {
            _templateManager = templateManager;
            _fields = new ConcurrentDictionary<int, IField>();
        }

        public IField Get(int id)
        {
            lock (this)
            {
                if (!_fields.ContainsKey(id))
                {
                    var template = _templateManager.Get<FieldTemplate>(id);

                    if (template == null) return null;
                    var field = new Field(template);


                    _fields[id] = field;
                }

                return _fields[id];
            }
        }

        public Task OnTick(DateTime now)
            => Task.WhenAll(_fields.Values.Select(field => field.OnTick(now)));
    }
}
