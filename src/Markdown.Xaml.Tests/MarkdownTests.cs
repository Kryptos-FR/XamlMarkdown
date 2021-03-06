﻿using System.Diagnostics;
using ApprovalTests;
using ApprovalTests.Reporters;
using NUnit.Framework;
using System.IO;
using System.Reflection;
using System.Windows.Markup;
using System.Xml;

namespace Markdown.Xaml.Tests
{
    [TestFixture]
    [UseReporter(typeof(DiffReporter))]
    public class MarkdownTests
    {
        [Test]
        public void Transform_givenTest1_generatesExpectedResult()
        {
            var text = LoadText("Test1.md");
            var markdown = new XamlMarkdown();
            var result = markdown.Transform(text);
            Approvals.Verify(AsXaml(result));
        }

        [Test]
        [RequiresSTA]
        public void Transform_givenLists_generatesExpectedResult()
        {
            var text = LoadText("Lists.md");
            var markdown = new XamlMarkdown();
            var result = markdown.Transform(text);
            Approvals.Verify(AsXaml(result));
        }

        [Test]
        [RequiresSTA]
        public void Transform_givenHorizontalRules_generatesExpectedResult()
        {
            var text = LoadText("HorizontalRules.md");
            var markdown = new XamlMarkdown();
            var result = markdown.Transform(text);
            Approvals.Verify(AsXaml(result));
        }

        [Test]
        [RequiresSTA]
        public void Transform_givenLinksInline_generatesExpectedResult()
        {
            var text = LoadText("Links_inline_style.md");
            var markdown = new XamlMarkdown();
            var result = markdown.Transform(text);
            Approvals.Verify(AsXaml(result));
        }

        [Test]
        [RequiresSTA]
        public void Transform_givenTextStyles_generatesExpectedResult()
        {
            var text = LoadText("Text_style.md");
            var markdown = new XamlMarkdown();
            var result = markdown.Transform(text);
            Approvals.Verify(AsXaml(result));
        }

        private static string LoadText(string name)
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Markdown.Xaml.Tests." + name))
            {
                Debug.Assert(stream != null, "stream != null");
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private static string AsXaml(object instance)
        {
            using (var writer = new StringWriter())
            {
                var settings = new XmlWriterSettings { Indent = true };
                using (var xmlWriter = XmlWriter.Create(writer, settings))
                {
                    XamlWriter.Save(instance, xmlWriter);
                }

                writer.WriteLine();
                return writer.ToString();
            }
        }
    }
}
