using System.Linq;
using AutoMapper;
using GrammarWorkbook.Data.Models;
using GrammarWorkbook.UnitTests.Utils;
using Xunit;

namespace GrammarWorkbook.UnitTests.Mappers
{
    public class MappersTests
    {
        private IMapper _mapper = MapperFactory.GetMapper();

        [Fact]
        public void SentenceToDto()
        {
            var sentence = new Sentence()
            {
                Text = "What (is|is,are) your (name|name,gender)?"
            };

            // Act
            var actual = _mapper.Map<GrammarWorkbook.UseCases.Units.Dto.Sentence>(sentence);
            
            // Assert
            Assert.Equal(5, actual.Words.Count);
            Assert.Equal("What ", actual.Words.ElementAt(0).Text);
            Assert.Empty(actual.Words.ElementAt(0).Options);
            Assert.True(actual.Words.ElementAt(1).IsBlank);
            Assert.Equal(2, actual.Words.ElementAt(1).Options.Length);
            Assert.Equal(" your ", actual.Words.ElementAt(2).Text);
            Assert.Empty(actual.Words.ElementAt(2).Options);
            Assert.True(actual.Words.ElementAt(3).IsBlank);
            Assert.Equal(2, actual.Words.ElementAt(3).Options.Length);
            Assert.Equal("?", actual.Words.ElementAt(4).Text);
            Assert.Empty(actual.Words.ElementAt(4).Options);
            
        }

    }
}