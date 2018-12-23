using System;

namespace GrammarWorkbook.Data.Dto
{
    public class SentenceDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public bool IsFullWidth { get; set; }
    }
}