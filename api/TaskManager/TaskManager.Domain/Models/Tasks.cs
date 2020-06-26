using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Domain.Models
{
    [Table("Task")]
    public class Tasks
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }

        public bool Concluded { get; set; }

        public string Description { get; set; }
    }
}
