using System;
using System.Collections.Generic;

namespace GrammarWorkbook.UseCases.Units.Dto
{
    public class Exercise
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public bool IsDialog { get; set; }
        public string[] Options { get; set; }
        public ICollection<Sentence> Sentences { get; set; }
    }
}