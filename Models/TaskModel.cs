using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Motivator.Models
{
    public class TaskModel
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(UserModel.Id))]
        public int OwnerId { get; set; }

        [ForeignKey(nameof(Id))]
        public int? SubTaskId { get; set; }

        public DateTime? DueDate { get; set; }

        [Required(ErrorMessage = "Task must have a title")]
        public string Title { get; set; }

        public string Description { get; set; }
    }
}
