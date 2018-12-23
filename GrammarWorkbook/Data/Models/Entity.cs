using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrammarWorkbook.Data.Models
{
    public class Entity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
    }
}