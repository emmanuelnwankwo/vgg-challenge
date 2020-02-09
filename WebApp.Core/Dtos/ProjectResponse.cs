using System;
using System.Collections.Generic;
using System.Text;

namespace WebApp.Core.Dtos
{
    public class ProjectResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public ProjectResponseData ResponseData { get; set; }
    }
    public class ProjectsResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public List<ProjectResponseData> ResponseData { get; set; }
    }
    public class ProjectResponseData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Completed { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
