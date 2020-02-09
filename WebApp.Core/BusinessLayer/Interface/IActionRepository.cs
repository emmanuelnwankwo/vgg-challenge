using System.Collections.Generic;
using WebApp.Core.Dtos;

namespace WebApp.Core.BusinessLayer.Interface
{
    public interface IActionRepository
    {
        ActionResponseData CreateAction(int id, ActionRequest actionRequest);
        bool DeleteAction(int projectId, int actionId);
        List<ActionResponseData> GetAllActions();
        List<ActionResponseData> GetAllActionsInProject(int id);
        ActionResponseData GetOneAction(int id);
        ActionResponseData GetActionInProject(int projectId, int actionId);
        ActionResponseData GetActionInProject(int projectId, int actionId, ActionRequest actionRequest);
        bool AccessAuthentication(string token);
    }
}