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
        public ICollection<Sentence> Sentences { get; private set; }

        public FillTheBlanksExercise()
        {
            Sentences = new List<Sentence>();
        }
    }

    public class DialogExercise : Exercise
    {
        public ICollection<Dialog> Dialogs { get; private set; }

        public DialogExercise()
        {
            Dialogs = new List<Dialog>();
        }
    }
    public class MatchTheWordsExercise : Exercise { }
}