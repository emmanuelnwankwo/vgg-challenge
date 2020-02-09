using System;
using System.Collections.Generic;
using System.Text;

namespace WebApp.Data.Models.Entities
{
    public class SessionRecord
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public Guid Token { get; set; }
    }
}
