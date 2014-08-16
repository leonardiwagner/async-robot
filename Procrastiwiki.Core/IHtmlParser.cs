using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Procrastiwiki.Model.Html;

namespace Procrastiwiki.Core
{
    public interface IHtmlParser
    {
        String Url { get; }
        Tag ReadTagValue(String tagName);
        IEnumerable<Tag> ReadTagsByType(String tagType);
        IEnumerable<String> SearchValidLinks();
    }
}
