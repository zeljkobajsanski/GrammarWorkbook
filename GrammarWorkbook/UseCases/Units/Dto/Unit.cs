using System;
using System.Collections.Generic;

namespace GrammarWorkbook.UseCases.Units.Dto
{
    public class Unit
    {
        public Guid UnitId { get; set; }
        public Guid? PreviousUnitId { get; set; }
        public Guid? NextUnitId { get; set; }
        public string Title { get; set; }
        public ICollection<Topic> Topics { get; set; }
    }
}