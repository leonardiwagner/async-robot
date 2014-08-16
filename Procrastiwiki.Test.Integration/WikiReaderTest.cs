using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Procrastiwiki.Core;

namespace Procrastiwiki.Test.Integration
{
    [TestFixture]
    public class WikiReaderTest
    {
        private Model.Wiki.Page wikiPage;

        [SetUp, Test]
        public void ReadWikiPage()
        {   
            var wikiReader = new Core.WikiReader();
            var htmlParser = new HtmlParser("http://en.wikipedia.org/wiki/Procrastination");
            wikiPage = wikiReader.ReadFromUrl(htmlParser);
            
            wikiPage.Should().NotBeNull();
        }

        [Test]
        public void SearchRelatedWiki()
        {
            
            foreach (var link in wikiPage.Links)
            {
                var htmlParser = new HtmlParser("http://en.wikipedia.org" + link);
                var wikiReader = new Core.WikiReader();
                var wp = wikiReader.ReadFromUrl(htmlParser);
                foreach (var lk in wp.Links)
                {
                    var hm = new HtmlParser("http://en.wikipedia.org" + lk);
                    var wk = new Core.WikiReader();
                    var wp2 = wk.ReadFromUrl(hm);

                    Debug.Print(wp2.Name);
                }
                Debug.Print(wp.Name);
            }
        }
    }
}
