using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Procrastiwiki.Core;

namespace Procrastiwiki.Test.Unit
{
    [TestFixture]
    public class HtmlReaderTest
    {
        public IHtmlReader htmlReader;

        [SetUp, Test]
        public void LoadHtmlToTest()
        {
            htmlReader = new HtmlReader(
                new StringBuilder("<html>")
                          .Append("  <head>")
                          .Append("    <title>This is the page title</title>")
                          .Append("  </head>")
                          .Append("  <body>")
                          .Append("    <a href=\"/Wiki/TestPage\">Test Page Title</a>")
                          .Append("    <a href=\"/Wiki/TestPage2\">Test Page Title2</a>")
                          .Append("  </body>")
                          .Append("</html>")
                .ToString()
            );
        }

        [Test]
        public void HtmlReaderShouldReadTagValue()
        {
            var titleTag = htmlReader.readTagValue("title");
            
            titleTag.Name.Should().Be("title");
            titleTag.Value.Should().Be("This is the page title");
        }

        [Test]
        public void HtmlReaderShouldReadLinkTags()
        {
            var linkTags = htmlReader.readTagsByType("a");
            
            linkTags.Count().Should().Be(2);
        }

        [Test]
        public void HtmlReaderShouldReadCorrectLinkTagValues()
        {
            var linkTags = htmlReader.readTagsByType("a");
            var linkTag = linkTags.Where(x => x.Value == "Test Page Title2").First();

            linkTag.GetProperties().Where(x => x.Key == "href").First().Value
                .Should().Be("/Wiki/TestPage2");
        }
    }
}
