using System;
using System.Collections.Generic;

namespace GrammarWorkbook.Data.Dto
{
    public class TopicDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public Guid UnitId { get; set; }
        public ICollection<ExerciseDto> Exercises { get; set; }
    }
}