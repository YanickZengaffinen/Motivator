using Motivator.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Motivator.DB.Models
{
    public class Task : ITask
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(User.Id))]
        public int OwnerId { get; set; }

        [ForeignKey(nameof(Id))]
        public int? SubTaskId { get; set; }

        public DateTime? DueDate { get; set; }

        [Required(ErrorMessage = "Task must have a title")]
        public string Title { get; set; }

        public string Description { get; set; }
    }
}
