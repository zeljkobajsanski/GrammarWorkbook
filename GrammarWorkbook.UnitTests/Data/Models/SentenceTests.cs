using System.Linq;
using GrammarWorkbook.Data.Models;
using Xunit;

namespace GrammarWorkbook.UnitTests.Data.Models
{
    public class SentenceTests
    {
        [Fact]
        public void ExtractPlaceholders_OneMatch()
        {
            var sentence = new Sentence()
            {
                Text = "How (are) you?"
            };
            var placeholders = sentence.ExtractPlaceholders();
            
            Assert.Single(placeholders);
            Assert.Equal("are", placeholders.ElementAt(0));
        }
        
        [Fact]
        public void ExtractPlaceholders_NoPlaceholders()
        {
            var sentence = new Sentence()
            {
                Text = "How are you?"
            };
            var placeholders = sentence.ExtractPlaceholders();
            
            Assert.Empty(placeholders);
        }
        
        [Fact]
        public void ExtractPlaceholders_PlaceholderOnTheBeginning()
        {
            var sentence = new Sentence()
            {
                Text = "(How) are you?"
            };
            var placeholders = sentence.ExtractPlaceholders();
            
            Assert.Single(placeholders);
            Assert.Equal("How", placeholders.ElementAt(0));
        }
        
        [Fact]
        public void ExtractPlaceholders_PlaceholderAtTheEnd()
        {
            var sentence = new Sentence()
            {
                Text = "How are (you)"
            };
            var placeholders = sentence.ExtractPlaceholders();
            
            Assert.Single(placeholders);
            Assert.Equal("you", placeholders.ElementAt(0));
        }

        [Fact]
        public void TokenizeWords_OneEmpty()
        {
            var s = new Sentence {Text = "How are (you)?"};
            var words = new string[] {"How are ", "_", "?"};
            var tokens = s.TokenizeWords();
            
            Assert.Equal(words.Length, tokens.Count());
            for (int i = 0; i < tokens.Count; i++)
            {
                Assert.Equal(words[i], tokens.ElementAt(i).Text);
                Assert.Empty(tokens.ElementAt(i).Options);
            }
        }
        
        [Fact]
        public void TokenizeWords_TwoEmpty()
        {
            var s = new Sentence {Text = "What (is) your (name)?"};
            var words = new string[] {"What ", "_", " your ", "_", "?"};
            var tokens = s.TokenizeWords();
            
            Assert.Equal(words.Length, tokens.Count());
            for (int i = 0; i < tokens.Count; i++)
            {
                Assert.Equal(words[i], tokens.ElementAt(i).Text);
                Assert.Empty(tokens.ElementAt(i).Options);
            }
        }

        [Fact]
        public void Tokenize_WithOptions()
        {
            var sentence = new Sentence() {Text = "What (is|are,is) your (name|name,gender)?"};
            
            // Act
            var tokens = sentence.TokenizeWords();
            
            // Assert
            Assert.Equal(5, tokens.Count);
            Assert.Equal("What ", tokens.ElementAt(0).Text);
            Assert.Equal("_", tokens.ElementAt(1).Text);
            Assert.Equal(" your ", tokens.ElementAt(2).Text);
            Assert.Equal("_", tokens.ElementAt(3).Text);
            Assert.Equal("?", tokens.ElementAt(4).Text);
            
            Assert.Equal(2, tokens.ElementAt(1).Options.Length);
            Assert.Equal("are", tokens.ElementAt(1).Options[0]);
            Assert.Equal("is", tokens.ElementAt(1).Options[1]);
            Assert.Equal(2, tokens.ElementAt(3).Options.Length);
            Assert.Equal("name", tokens.ElementAt(3).Options[0]);
            Assert.Equal("gender", tokens.ElementAt(3).Options[1]);
        }
    }
}