using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TTI.Practicas.Data
{
    public class TodoItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }

        public Guid? CategoryId { get; set; }
        public Category Category { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    }
}
