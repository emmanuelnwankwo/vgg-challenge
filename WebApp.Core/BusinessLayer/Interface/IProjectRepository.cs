using System.Collections.Generic;
using WebApp.Core.Dtos;

namespace WebApp.Core.BusinessLayer.Interface
{
    public interface IProjectRepository
    {
        ProjectResponseData CreateProject(ProjestRequest projestRequest);
        ProjectResponseData GetOneProject(int id);
        List<ProjectResponseData> GetAllProjects();
        bool DeleteProject(int id);
        ProjectResponseData UpdateCompleted(int id, ProjestPatchRequest projestPatchRequest);
        ProjectResponseData UpdateAll(int id, ProjestRequest projestRequest);
        bool AccessAuthentication(string token);
    }
}