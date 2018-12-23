using System.Collections.Generic;

namespace GrammarWorkbook.Data.Models
{
    public class Sentence : Entity
    {
        public string Text { get; set; }
        public bool IsFullWidth { get; set; }

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

        public ICollection<string> TokenizeWords()
        {
            var tokens = new List<string>();
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
                        tokens.Add(Text.Substring(beginningIndex + 1));
                        break;
                    }
                    tokens.Add(Text.Substring(beginningIndex + 1, index - beginningIndex - 1));
                    openedBracketsFound = true;
                }
                else
                {
                    index = Text.IndexOf(')', beginningIndex);
                    if (index == -1) break;
                    tokens.Add("_");
                    openedBracketsFound = false;
                }
                beginningIndex = index;
            }

            return tokens;
        }
    }
}