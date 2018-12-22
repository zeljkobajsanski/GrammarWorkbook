using System;
using System.Collections.Generic;

namespace GrammarWorkbook.Data.Models
{
    public class Topic : Entity
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }

        public Guid UnitId { get; set; }
        public Unit Unit { get; set; }

        public ICollection<Exercise> Exercises { get; private set; }

        public Topic()
        {
            Exercises = new List<Exercise>();
        }
    }
}