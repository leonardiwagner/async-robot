using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procrastiwiki.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            var wikiReader = new Core.WikiReader();
            var htmlParser = new Core.HtmlParser("http://en.wikipedia.org/wiki/Procrastination");

            wikiReader.SearchRelatedLinksT(htmlParser, 2);

            System.Console.Read();
        }
    }
}
