using System.Collections.Generic;
using GrammarWorkbook.UseCases.Units.Dto;
using Xunit;

namespace GrammarWorkbook.UnitTests.UseCases.Units.Dto
{
    public class SentenceTests
    {
        [Fact]
        public void MakeSentence()
        {
            // Arrange
            var sentence = new Sentence
            {
                Words = new List<Word>()
                {
                    new Word() {Text = "What "}, new Word() {IsBlank = true, Text = "is"},
                    new Word() {Text = " your "}, new Word() {Text = "name", IsBlank = true}
                }
            };

            // Act
            var actual = sentence.MakeSentence();

            // Assert
            Assert.Equal("What (is) your (name)", actual);
        }
    }
}