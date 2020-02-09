using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebApp.Core.Dtos
{
    public class ActionRequest
    {
        [Required]
        public string Note { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
