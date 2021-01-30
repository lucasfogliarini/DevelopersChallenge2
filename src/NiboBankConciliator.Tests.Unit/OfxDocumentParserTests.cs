using Xunit;
using NiboBankConciliator.Core;
using System.IO;
using System;

namespace NiboBankConciliator.Tests.Unit
{
    public class OfxDocumentParserTests
    {
        [Fact]
        public void Parse_Should_Parse()
        {
            //Given
            var extrato1OfxDocment = Path.Combine(Environment.CurrentDirectory, "extrato1.ofx");
            var ofxDocumentStream = File.OpenRead(extrato1OfxDocment);

            //When
            var ofxDocument = OfxDocumentParser.Parse(ofxDocumentStream);

            //Then
            Assert.NotNull(ofxDocument);
        }
    }
}
