using System.Collections.Generic;
using System.Text;

namespace GrammarWorkbook.Data.Models
{
    public class Sentence : Entity
    {
        public string Text { get; set; }

        public ICollection<string> ExtractPlaceholders()
        {
            if (string.IsNullOrEmpty(Text)) return new string[0];
            var placeholders = new List<string>();
            int index = 0;
            while ((index = Text.IndexOf('(', index)) != -1)
            {
                var closedBracketIndex = Text.IndexOf(')', index);
                if (closedBracketIndex != -1)
                {
                    placeholders.Add(Text.Substring(index + 1, closedBracketIndex - index - 1));
                    index = closedBracketIndex;
                }
            }

            return placeholders;
        }

        public ICollection<Token> TokenizeWords()
        {
            var tokens = new List<Token>();
            int index = 0;
            int beginningIndex = -1;
            bool openedBracketsFound = false;
            while (true)
            {
                if (!openedBracketsFound)
                {
                    index = Text.IndexOf('(', beginningIndex + 1);
                    if (index == -1)
                    {
                        tokens.Add(new Token()
                        {
                            Text = Text.Substring(beginningIndex + 1),
                            Options = new string[0],
                            CorrectOption = Text.Substring(beginningIndex + 1)
                        });
                        break;
                    }
                    tokens.Add(new Token()
                    {
                        Text = Text.Substring(beginningIndex + 1, index - beginningIndex - 1), 
                        Options = new string[0],
                        CorrectOption = Text.Substring(beginningIndex + 1, index - beginningIndex - 1)
                    });
                    openedBracketsFound = true;
                }
                else
                {
                    index = Text.IndexOf(')', beginningIndex);
                    if (index == -1) break;
                    var token = Text.Substring(beginningIndex + 1, index - beginningIndex - 1);
                    tokens.Add(new Token{Text = "_", Options = GetOptions(token), CorrectOption = GetCorrectOption(token)});
                    openedBracketsFound = false;
                }
                beginningIndex = index;
            }

            return tokens;
        }

        private string[] GetOptions(string token)
        {
            var parts = token.Split('|');
            if (parts.Length == 2)
            {
                return parts[1].Split(',');
            }
            return new string[0];
        }

        private string GetCorrectOption(string token)
        {
            var parts = token.Split('|');
            if (parts.Length == 1)
            {
                return token;
            }
            if (parts.Length == 2)
            {
                return parts[0];
            }
            return null;
        }

        public string GetCorrectAnswer()
        {
            var tokens = TokenizeWords();
            var sb = new StringBuilder();
            foreach (var token in tokens)
            {
                sb.Append(token.Text == "_" ? $"({token.CorrectOption})" : token.Text);
            }
            return sb.ToString();
        }
    }
}