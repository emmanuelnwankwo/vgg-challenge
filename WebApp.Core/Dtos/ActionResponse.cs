using System;
using System.Collections.Generic;
using System.Text;

namespace WebApp.Core.Dtos
{
    public class ActionResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public ActionResponseData ResponseData { get; set; }
    }

    public class ActionsResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public IEnumerable<ActionResponseData> ResponseData { get; set; }
    }
    public class ActionResponseData
    {
        public int Id { get; set; }
        public int Project_Id { get; set; }
        public string Note { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
