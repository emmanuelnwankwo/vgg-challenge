using System;
using System.Collections.Generic;
using System.Text;

namespace WebApp.Core.Dtos
{
    public class ErrorResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
    }
}
