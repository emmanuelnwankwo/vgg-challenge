using System.Collections.Generic;
using WebApp.Core.Dtos;
using WebApp.Core.Utility;
using WebApp.Data.Models.Entities;

namespace WebApp.Core.BusinessLayer.Interface
{
    public interface IProjectRepository
    {
        ProjectResponseData CreateProject(ProjestRequest projestRequest);
        ProjectResponseData GetOneProject(int id);
        PagedList<Project> GetAllProjects(ProjectResourceParameters projectResourceParameters);
        bool DeleteProject(int id);
        ProjectResponseData UpdateCompleted(int id, ProjestPatchRequest projestPatchRequest);
        ProjectResponseData UpdateAll(int id, ProjestRequest projestRequest);
    }
}