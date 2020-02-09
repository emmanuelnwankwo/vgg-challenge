
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebApp.Data.Models.Entities
{
    public class Action
    {
        public int Id { get; set; }
        [Required]
        public int Project_Id { get; set; }
        public Project Project { get; set; }
        [Required]
        public string Note { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
