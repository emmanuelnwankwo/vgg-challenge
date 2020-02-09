using System;
using System.Collections.Generic;
using System.Text;

namespace WebApp.Core.Dtos
{
    public class UserResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public UserResponseData ResponseData { get; set; }
    }
    public class UserResponseData
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
