using System;
using System.Collections.Generic;

namespace GrammarWorkbook.UseCases.Units.Dto
{
    public class Topic
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public ICollection<Exercise> Exercises { get; set; }
    }
}