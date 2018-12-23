using System;
using System.Collections.Generic;

namespace GrammarWorkbook.Data.Models
{
    public abstract class Exercise : Entity
    {
        public string Title { get; set; }
        public Topic Topic { get; set; }
        public Guid TopicId { get; set; }
        public bool UseOptions { get; set; }
    }

    public class FillTheBlanksExercise : Exercise
    {
        public bool IsDialog { get; set; }
        public ICollection<Sentence> Sentences { get; private set; }

        public FillTheBlanksExercise()
        {
            Sentences = new List<Sentence>();
        }
    }

    public class MatchTheWordsExercise : Exercise { }
}