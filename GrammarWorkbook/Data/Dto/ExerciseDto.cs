using System;
using System.Collections.Generic;

namespace GrammarWorkbook.Data.Dto
{
    public class ExerciseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid TopicId { get; set; }
        public string Type { get; set; }
        public bool UseOptions { get; set; }
        public bool IsDialog { get; set; }
        public ICollection<SentenceDto> Sentences { get; set; }
    }
}