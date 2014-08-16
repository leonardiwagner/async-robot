using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procrastiwiki.Model.Html
{
    public class Tag
    {
        public String Name { get; set; }
        public String Value { get; set; }
        private IDictionary<string, string> properties;

        public Tag(string name, string value)
        {
            Name = name;
            Value = value;
            properties = new Dictionary<string, string>();
        }

        public void AddProperty(String name, String value)
        {
            properties.Add(name, value);   
        }

        public IEnumerable<KeyValuePair<string, string>> GetProperties()
        {
            return properties;
        }
    }
}
