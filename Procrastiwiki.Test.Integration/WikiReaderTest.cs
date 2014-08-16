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
            var wikiReader = new Core.WikiReader();
            var htmlParser = new HtmlParser("http://en.wikipedia.org/wiki/Procrastination");

            var result  = wikiReader.SearchRelatedLinks(htmlParser, 2);
            result.Count.Should().Be(30);
        }
    }
}
