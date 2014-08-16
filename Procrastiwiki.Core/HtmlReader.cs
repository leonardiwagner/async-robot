using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procrastiwiki.Core
{
    public class HtmlReader : IHtmlReader
    {
        public HtmlReader(string htmlBody)
        {
            
        }
        public Model.Html.Tag readTagValue(string tagName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Model.Html.Tag> readTagsByType(string tagType)
        {
            throw new NotImplementedException();
        }
    }
}
