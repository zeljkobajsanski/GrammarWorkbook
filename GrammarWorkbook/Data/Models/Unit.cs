using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GrammarWorkbook.Data.Models
{
    public class Unit : Entity
    {
        [StringLength(255), Required]
        public string Title { get; set; }
        public ICollection<Topic> Topics { get; private set; }

        public Unit()
        {
            Topics = new List<Topic>();
        }
    }
}