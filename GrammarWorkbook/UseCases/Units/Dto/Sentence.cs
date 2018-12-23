using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrammarWorkbook.UseCases.Units.Dto
{
    public class Sentence
    {
        public Guid Id { get; set; }
        public ICollection<Word> Words { get; set; }
        public bool IsCorrect { get; set; }
        public string CorrectText { get; set; }

        public string MakeSentence()
        {
            var sb = new StringBuilder();
            foreach (var word in Words)
            {
                sb.Append(word.IsBlank ? $"({word.Text})" : word.Text);
            }

            return sb.ToString();
        }
    }
}