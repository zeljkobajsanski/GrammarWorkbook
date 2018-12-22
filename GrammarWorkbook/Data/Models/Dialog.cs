using System.Collections.Generic;

namespace GrammarWorkbook.Data.Models
{
    public class Dialog : Entity
    {
        public Sentence Sentence { get; set; }
        public ICollection<Option> Options { get; set; }
    }
}