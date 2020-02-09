using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebApp.Core.Dtos
{
    public class ProjestRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public bool? Completed { get; set; }
    }

    public class ProjestPatchRequest
    {
        [Required]
        public bool Completed { get; set; }
    }
}
