using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

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
            wikiPage = wikiReader.ReadFromUrl("http://en.wikipedia.org/wiki/Procrastination");
            
            wikiPage.Should().NotBeNull();
        }
    }
}
